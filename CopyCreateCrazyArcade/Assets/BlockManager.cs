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

        //// ���� ����Ʈ
        //blockList = new List<Vector3>();
        //diction = new Dictionary<Vector3Int, bool>();

        //for (int n = _tilemap.cellBounds.xMin; n < _tilemap.cellBounds.xMax; n++)
        //{
        //    for (int p = _tilemap.cellBounds.yMin; p < _tilemap.cellBounds.yMax; p++)
        //    {
        //        Vector3Int localPlace = (new Vector3Int(n, p, 0));

        //        Vector3 place = _tilemap.CellToLocal(localPlace);

        //        //Debug.Log($"n : {n}, p : {p}");
        //        Debug.Log($"�������� �Ѱ� : {place}");
        //        Debug.Log($"���� �÷��̽� {localPlace}");

        //        // �����ǿ� Ÿ���� �ִٸ� �߰�
        //        if (_tilemap.HasTile(localPlace))
        //        {
        //            blockList.Add(localPlace);
        //            diction.Add(localPlace, false);
        //            Debug.Log("����");
        //        }
        //        else
        //        {
        //            Debug.Log("�Ѱ�");
        //            continue;

        //        }
        //    }
        //}
    }
    Vector3 selfPo;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"�浹 ���� : {collision.GetContact(0)}");


        if (collision.gameObject.CompareTag("Explosion"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector3 localPoint = _tilemap.WorldToCell(contact.point);





            Vector3Int removeCell = _tilemap.LocalToCell(localPoint);
          
            Debug.Log($"{contact.point} ����Ʈ");
            Debug.Log($"{localPoint} ���� ����Ʈ");
            Debug.Log($"{removeCell} ������ ��");
            removeCell.x = removeCell.x - 1;

            Debug.Log(removeCell);
            _tilemap.SetTile(removeCell, null);
        }
    }
}
