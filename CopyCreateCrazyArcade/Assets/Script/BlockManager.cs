using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Script
{
    public class BlockManager : MonoBehaviour
    {
        private Collider2D _collider;
        private Tilemap _tilemap;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _tilemap = transform.GetComponent<Tilemap>();
        }

        // ���� ���� ������ �ν��Ͻ��� ����
        private int itemCount = 1;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Explosion"))
            {

                Vector2 asdas = collision.transform.position;

                Vector3Int[] asd = new Vector3Int[4];
                Debug.Log(asdas);

                for (int i = 0; i < asd.Length; ++i)
                {
                    asd[i] = _tilemap.WorldToCell(asdas);


                }
                asd[0].x = asd[0].x + itemCount;
                asd[1].x = asd[1].x - itemCount;
                asd[2].y = asd[2].y + itemCount;
                asd[3].y = asd[3].y - itemCount;


                for (int i = 0; i < asd.Length; ++i)
                {
                    Debug.Log(asd[i]);
                    if (_tilemap.HasTile(asd[i]))
                    {
                        _tilemap.SetTile(asd[i], null);

                    }
                }

                //Vector2 collisionPoint = collision.ClosestPoint(transform.position);
                //Vector3Int removeTilePosition = _tilemap.WorldToCell(collisionPoint);
                //Debug.Log($"{removeTilePosition} : ������ ������, ���� �� ��");
                // _tilemap.SetTile(removeTilePosition, null);


            // ��ǳ���� ��ġ�� ����� ��, �� �Ͻ� �ı� ����
            // ��ǳ���� ��ġ�� ����� ��, �� �Ͻ� �ı� X

            }

        }
    }
}