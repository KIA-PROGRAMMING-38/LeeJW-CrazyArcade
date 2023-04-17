using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Assets.Script
{
    public class PlayerAttack : MonoBehaviour
    {
        PlayerInput _input;
        PlayerStatus _status;
        public WaterBalloon _Balloon;
        private WaitForSeconds waitBalloon = new WaitForSeconds(3);
        bool kickOn = false;
        private Vector3 selfposition;
        private bool _isBalloonOnTile = true;
        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _status = GetComponent<PlayerStatus>();
        }



        private void Update()
        {

            if (gameObject.name == "1PCharacter" && _input.FirstPlayerAttack() && _isBalloonOnTile && _status.currentBalloonCount > 0)
            {
                StartCoroutine(CreateBalloon());
            }
            if (gameObject.name == "2PCharacter" && _input.SecondPlayerAttack() && _isBalloonOnTile && _status.currentBalloonCount > 0)
            {
                StartCoroutine(CreateBalloon());
            }
        }
        private IEnumerator CreateBalloon()
        {
            --_status.currentBalloonCount;

            // 현재 포지션을 셀포지션으로
            selfposition = MapManager.Instance.LocalToCellPosition(transform);

            if (selfposition.x > -10f)
            {
                selfposition.y = selfposition.y + 0.05f;
                WaterBalloon balloon = Instantiate(_Balloon, selfposition, Quaternion.identity);
                balloon.currentPower = _status.currentExplosionPower;
            }

            yield return waitBalloon;
                ++_status.currentBalloonCount;


        }
        Vector2 normalVec = Vector2.zero;

        //노말벡터 및 공식 적용
        ContactPoint2D[] point = new ContactPoint2D[1];
        private void SetVector(Collision2D collision)
        {
            collision.GetContacts(point);
            normalVec = point[0].normal * -1;
            normalVec = normalVec * 8;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 타일 위에 불룬이 있는 판별
            if (collision.gameObject.CompareTag("Balloon"))
            {
                _isBalloonOnTile = false;
            }
            if (collision.CompareTag("ItemShoes"))
            {
                kickOn = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Balloon"))
            {
                _isBalloonOnTile = true;
            }
        }

    }
}