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

        // 추후 실제 아이템 인스턴스로 변동
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
                //Debug.Log($"{removeTilePosition} : 삭제할 포지션, 월드 투 셀");
                // _tilemap.SetTile(removeTilePosition, null);


            // 물풍선의 위치가 블록의 상, 좌 일시 파괴 정상
            // 물풍선의 위치가 블록의 하, 우 일시 파괴 X

            }

        }
    }
}