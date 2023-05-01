using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Assets.Script
{
    public class BlockManager : MonoBehaviour
    {

        private GameData _gameData;
        public TakeItem[] _item;
        Collider2D[] target = new Collider2D[2];
        private ObjectPool<TakeItem> itemPool;
        private int _itemRandomValue;
        private int _itemRandomSpawn;
        private Collider2D _collider;
        private Vector3 savePosition;
        

        private void Awake()
        {
            _gameData = FindObjectOfType<GameData>();
            itemPool = new ObjectPool<TakeItem>(CreatePoolItem, TakeItemFromPool, ReturnItemToPool,
              (item) => Destroy(item.gameObject), 30, 50);

            _collider = GetComponent<Collider2D>();
            savePosition = transform.position;
            savePosition.y = savePosition.y + 0.2f;
        }

        private bool isMoving = false;
        private float moveTime = 0;
        private bool _trigger = true;

        private void Update()
        {
            BlockMovement();
        }
        private Vector3 spawnPosition;

        private void ItemCreate()
        {
            _itemRandomSpawn = Random.Range(0, 10);

            spawnPosition.x = transform.position.x;
            spawnPosition.y = transform.position.y + 0.2f;

            if (_itemRandomSpawn < 6)
                GetItemFromPool();
        }


        private void BlockClearEvent()
        {
            ItemCreate();
            gameObject.SetActive(false);
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

                        if (elapsedTime >= 0.5)
                        {
                            isMoving = true;
                            elapsedTime = 0;
                            target[0].gameObject.SetActive(false);
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

        private TakeItem CreatePoolItem()
        {
            _itemRandomValue = Random.Range(0, _item.Length);

            if (_gameData.defaultMode)
            {
                TakeItem item = Instantiate(_item[_itemRandomValue], savePosition, transform.rotation);
                item.Pool = itemPool;
                return item;
            }

            else
            {
                TakeItem item = Instantiate(_item[_itemRandomValue - 3], savePosition, transform.rotation);
                item.Pool = itemPool;
                return item;
            }

        }
        private TakeItem GetItemFromPool()
        {
            Debug.Assert(itemPool != null);
            TakeItem item = itemPool.Get();
            return item;
        }
        private void TakeItemFromPool(TakeItem item) => item.gameObject.SetActive(true);
        private void ReturnItemToPool(TakeItem item) => item.gameObject.SetActive(false);
    }
}