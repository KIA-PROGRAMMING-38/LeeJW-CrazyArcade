using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAttack : MonoBehaviour
{
    PlayerInput _input;
    private GameObject _Balloon;
    public Grid _grid;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _Balloon = Resources.Load("Balloon") as GameObject;
        _grid = GetComponent<Grid>();
    }
    Vector3Int cellpo;

    private void Start()
    {
        transform.localPosition = _grid.GetCellCenterLocal(cellpo);
    }
    void Update()
    {

        cellpo = _grid.LocalToCell(transform.localPosition);

        if (_input.Attack())
        {
            Instantiate(_Balloon, cellpo, transform.rotation);
        }
    }


}
