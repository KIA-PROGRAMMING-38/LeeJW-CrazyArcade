using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public class BlockManager : MonoBehaviour
    {
        private Animator _anim;
        private Collider2D _collider;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        private bool isMoving = false;
        private float moveTime = 0;
        private bool _trigger = true;

        private float raycastDistance = 0.05f;

        Vector3 averageVec = new Vector3(0.5f, 0.5f, 0);
        RaycastHit2D hit;

        private float checkBlockTime;
        private void Update()
        {

            BlockMovement();
            //if(destroyAnim)
            //{
            //  checkBlockTime += Time.deltaTime;
            //    _anim.SetBool("BlockDestroy", true);

            //    if(checkBlockTime > 0.2)
            //    {
            //        Destroy(gameObject);
            //        checkBlockTime = 0;
            //    }
            //}

        }
        private bool destroyAnim = false;
        private float elapsedTime;
        Vector3 newPosition = Vector3.zero;
        Vector3 normalVec = Vector3.zero;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            normalVec = collision.contacts[0].normal;   

        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            normalVec = collision.contacts[0].normal;
            if (collision.gameObject.CompareTag("Player") && _collider.CompareTag("MovingBlock") && _trigger)
            {

                elapsedTime += Time.deltaTime;

                VectorIntTransform();

                if (elapsedTime >= 0.3)
                {

                    isMoving = true;
                    elapsedTime = 0;
                }
            }
        }

        private void VectorIntTransform()
        {
            normalVec.x = Mathf.RoundToInt(normalVec.x);
            normalVec.y = Mathf.RoundToInt(normalVec.y);
            normalVec.z = Mathf.RoundToInt(normalVec.z);

            newPosition = transform.position + normalVec;
        }

        private void BlockMovement()
        {
            if (isMoving)
            {
                _trigger = false;

                moveTime += Time.deltaTime;
                averageVec = normalVec / 2;
                // 노말벡터 앞에 물체가 있는지 판단.
                hit = Physics2D.Raycast(transform.position + averageVec, normalVec, raycastDistance);

                if (normalVec.x == 0 || normalVec.y == 0)
                {

                    if (hit == false)
                    {
                        // 보간으로 포지션 변경
                        transform.position = Vector3.Lerp(transform.position, newPosition, moveTime / 5f);

                        if (moveTime > 0.8f)
                        {
                            moveTime = 0;
                            isMoving = false;
                            _trigger = true;
                        }

                    }
                    else
                    {
                        transform.position = transform.position;
                        moveTime = 0;
                        isMoving = false;
                        _trigger = true;

                    }

                }


            }

        }
    }
}