using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    private Collider2D _collider;
    private Tilemap _tilemap;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _tilemap = transform.GetComponent<Tilemap>();
    }


    public List<Vector3> blockList;
    public Dictionary<Vector3Int, bool> diction;

    void Start()
    {

        //// 담을 리스트
        //blockList = new List<Vector3>();
        //diction = new Dictionary<Vector3Int, bool>();

        //for (int n = _tilemap.cellBounds.xMin; n < _tilemap.cellBounds.xMax; n++)
        //{
        //    for (int p = _tilemap.cellBounds.yMin; p < _tilemap.cellBounds.yMax; p++)
        //    {
        //        Vector3Int localPlace = (new Vector3Int(n, p, 0));

        //        Vector3 place = _tilemap.CellToLocal(localPlace);

        //        //Debug.Log($"n : {n}, p : {p}");
        //        Debug.Log($"셀투로컬 한것 : {place}");
        //        Debug.Log($"로컬 플레이스 {localPlace}");

        //        // 포지션에 타일이 있다면 추가
        //        if (_tilemap.HasTile(localPlace))
        //        {
        //            blockList.Add(localPlace);
        //            diction.Add(localPlace, false);
        //            Debug.Log("에드");
        //        }
        //        else
        //        {
        //            Debug.Log("넘겨");
        //            continue;

        //        }
        //    }
        //}
    }
    Vector3 selfPo;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"충돌 지점 : {collision.GetContact(0)}");


        if (collision.gameObject.CompareTag("Explosion"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector3 localPoint = _tilemap.WorldToCell(contact.point);





            Vector3Int removeCell = _tilemap.LocalToCell(localPoint);
          
            Debug.Log($"{contact.point} 컨택트");
            Debug.Log($"{localPoint} 로컬 포인트");
            Debug.Log($"{removeCell} 리무브 셀");
            removeCell.x = removeCell.x - 1;

            Debug.Log(removeCell);
            _tilemap.SetTile(removeCell, null);
        }
    }
}
