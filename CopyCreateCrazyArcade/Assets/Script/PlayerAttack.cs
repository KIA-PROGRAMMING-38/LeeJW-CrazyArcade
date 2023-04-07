using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAttack : MonoBehaviour
{
    PlayerInput _input;
    private GameObject _Balloon;
    public Tilemap _tilemap;
    public Grid _grid;
    public Grid grid;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _input = GetComponent<PlayerInput>();
        _Balloon = Resources.Load("Balloon") as GameObject;
        _grid = GetComponent<Grid>();
        grid = GetComponent<Grid>();
    }
    Vector3Int vecint = new Vector3Int(-536, -2, 0);
    public Vector3Int GetCellPositionFromWorld(Vector3 worldPosition) => _grid.LocalToCell(worldPosition);
    Vector3Int cellpo;

    private void Start()
    {

        transform.localPosition = grid.GetCellCenterLocal(cellpo);

    }
    void Update()
    {

        cellpo = grid.LocalToCell(transform.localPosition);
        Vector3Int cellPosition = GetCellPositionFromWorld(transform.position);

        if (_input.Attack())
        {
            Debug.Log($"ÇÃ·¹ÀÌ¾îÀÇ ÇöÀç À§Ä¡ÇÑ ¼¿ ÁÂÇ¥´Â : {cellPosition}");

            if (cellPosition == transform.position)
            {
                Debug.Log("°ø°Ý ¶Ñ¶×");
            }
        }


        if (_input.Attack())
        {
            Instantiate(_Balloon, cellpo, transform.rotation);
        }
    }


}
