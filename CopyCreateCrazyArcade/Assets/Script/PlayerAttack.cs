using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        private Rigidbody2D _rigid;
        private ObjectPool<WaterBalloon> _balloonPool;
        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _status = GetComponent<PlayerStatus>();
            _rigid = GetComponent<Rigidbody2D>();

            _balloonPool = new ObjectPool<WaterBalloon>(CreatePoolBalloon, TakeBalloonFromPool, ReturnBalloonToPool,
                (balloon) => Destroy(balloon.gameObject), 20, 100);
        }

     

        private void Update()
        {
            target[0] = Physics2D.OverlapBox(transform.position, Vector2.one, 0f);



            if (target[0].gameObject.CompareTag("Balloon") == false)
            {
                if (gameObject.name == "1PCharacter(Clone)" && _input.FirstPlayerAttack() && _status.currentBalloonCount > 0)
                {
                    StartCoroutine(CreateBalloon());
                }
                if (gameObject.name == "2PCharacter(Clone)" && _input.SecondPlayerAttack() && _status.currentBalloonCount > 0)
                {

                    StartCoroutine(CreateBalloon());
                }
            }

          


        }
        
        private IEnumerator CreateBalloon()
        {
            --_status.currentBalloonCount;

            selfposition = MapManager.Instance.LocalToCellPosition(transform);

            selfposition.y = selfposition.y + 0.05f;
            //WaterBalloon balloon = Instantiate(_Balloon, selfposition, Quaternion.identity);
            WaterBalloon balloon = GetBalloonFromPool();
            balloon.currentPower = _status.currentExplosionPower;


            yield return waitBalloon;
            ++_status.currentBalloonCount;

        }
        Vector3 normalVec = Vector3.zero;

        ContactPoint2D[] point = new ContactPoint2D[1];
        private void SetVector(Collision2D collision)
        {
            collision.GetContacts(point);
            normalVec = point[0].normal * -1;
            normalVec = normalVec * 10;
        }
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
                    _rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Balloon") && kickOn)
            {
                _rigid.constraints = RigidbodyConstraints2D.None;
                _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ItemShoes"))
            {
                kickOn = true;
            }
        }


        private WaterBalloon CreatePoolBalloon()
        {
            WaterBalloon balloon = Instantiate(_Balloon);
            balloon.Pool = _balloonPool;
            return balloon;
        }
        private WaterBalloon GetBalloonFromPool()
        {
            Debug.Assert(_balloonPool != null);
            WaterBalloon balloon = _balloonPool.Get();
            return balloon;
        }
        private void TakeBalloonFromPool(WaterBalloon balloon) => balloon.gameObject.SetActive(true);
        private void ReturnBalloonToPool(WaterBalloon balloon) => balloon.gameObject.SetActive(false);
    }
}