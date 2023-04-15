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
        private int attackCount = 1;
        private  WaitForSeconds waitBalloon = new WaitForSeconds(3);
        bool kickOn = false;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _status = GetComponent<PlayerStatus>();
            attackCount = _status.currentBalloonCount;
        }

        private Vector3 selfposition;
        private bool _isBalloonOnTile = true;

        private void Update()
        {
            if (_input.Attack() && _isBalloonOnTile && attackCount > 0)
            {
                StartCoroutine(CreateBalloon());
            }
        }
        private IEnumerator CreateBalloon()
        {
            --attackCount;
            // 현재 포지션을 셀포지션으로
            selfposition = MapManager.Instance.LocalToCellPosition(transform);

            if (selfposition.x > -10f)
            {
                WaterBalloon balloon = Instantiate(_Balloon, selfposition, Quaternion.identity);
                balloon.currentPower = _status.currentExplosionPower;
            }

            yield return waitBalloon;

            ++attackCount;
            
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
            // 아이템 습득시 스탯 증가
            if (collision.CompareTag("ItemBalloon") && _status.MAX_BALLOON_COUNT > attackCount)
            {
                attackCount += 1;
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