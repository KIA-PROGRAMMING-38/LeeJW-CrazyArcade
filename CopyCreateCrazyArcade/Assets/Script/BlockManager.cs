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


        GameObject _item;
        Collider2D[] target = new Collider2D[2];

        private Animator _anim;
        private Collider2D _collider;

        private void Awake()
        {
            _item = Resources.Load("Shoes") as GameObject;
            _anim = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        private bool isMoving = false;
        private float moveTime = 0;
        private bool _trigger = true;

        private void Update()
        {
            BlockMovement();
        }
        private Vector3 spawnPosition;
        private void OnDestroy()
        {
            spawnPosition.x = transform.position.x;
            spawnPosition.y = transform.position.y + 0.2f;

            Instantiate(_item, spawnPosition, transform.rotation);
        }
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
                if (target[0] = Physics2D.OverlapBox(transform.position + normalVec, Vector2.one / 5f, 0f))
                {

                    if (target[0].gameObject.layer == LayerMask.NameToLayer("Item"))
                    {
                        elapsedTime += Time.deltaTime;

                        VectorIntTransform();

                        if (elapsedTime >= 0.3)
                        {

                            isMoving = true;
                            elapsedTime = 0;
                            Destroy(target[0].gameObject);

                        }


                    }
                
                }

                else
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


                transform.position = Vector3.Lerp(transform.position, newPosition, moveTime / 6f);

                if (moveTime > 0.8f)
                {
                    moveTime = 0;
                    isMoving = false;
                    _trigger = true;
                }


            }

        }
    }
}