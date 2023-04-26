using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Assets.Script
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerStatus _status;
        public WaterBalloon _Balloon;
        private WaitForSeconds waitBalloon = new WaitForSeconds(3);
        private bool kickOn = false;
        private Vector3 selfposition;
        private Collider2D[] target = new Collider2D[2];


        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _status = GetComponent<PlayerStatus>();
        }



        private void Update()
        {
            target[0] = Physics2D.OverlapBox(transform.position, Vector2.one, 0f);



            if (target[0].gameObject.CompareTag("Balloon") == false)
            {
                if (gameObject.name == "1PCharacter" && _input.FirstPlayerAttack() && _status.currentBalloonCount > 0)
                {
                    StartCoroutine(CreateBalloon());
                }
                if (gameObject.name == "2PCharacter" && _input.SecondPlayerAttack() && _status.currentBalloonCount > 0)
                {

                    StartCoroutine(CreateBalloon());
                }
            }


        }
        private IEnumerator CreateBalloon()
        {
            --_status.currentBalloonCount;

            // 현재 포지션을 셀포지션으로
            selfposition = MapManager.Instance.LocalToCellPosition(transform);


            selfposition.y = selfposition.y + 0.05f;
            WaterBalloon balloon = Instantiate(_Balloon, selfposition, Quaternion.identity);
            balloon.currentPower = _status.currentExplosionPower;


            yield return waitBalloon;
            ++_status.currentBalloonCount;


        }
        Vector3 normalVec = Vector3.zero;

        //노말벡터 및 공식 적용
        ContactPoint2D[] point = new ContactPoint2D[1];
        private void SetVector(Collision2D collision)
        {
            collision.GetContacts(point);
            normalVec = point[0].normal * -1;
            normalVec = normalVec * 10;
        }
        // 제약해제
        private void SetConstraints(Collision2D collision)
        {
            collision.rigidbody.constraints = RigidbodyConstraints2D.None;
            collision.rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Balloon") && kickOn)
            {
                SetVector(collision);

                if (normalVec.x == 0 || normalVec.y == 0)
                {
                    SetConstraints(collision);
                    collision.rigidbody.velocity = normalVec;

                }
            }
        }
        private void bubbleMove(Collision2D collision)
        {

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ItemShoes"))
            {
                kickOn = true;
            }
        }

    }
}