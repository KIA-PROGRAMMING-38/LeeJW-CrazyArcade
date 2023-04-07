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
    Vector3 cellpo;
    Vector3 selfposition;

    void Update()
    {
        // cell ����� ���� ����ϰ� ��µǴ� �κ� ������ �������� �ڿ������� ��ġ.
        selfposition = transform.position;
        selfposition.x = selfposition.x + 0.42f;
        cellpo = _grid.LocalToCell(selfposition);


        Debug.Log(cellpo.x);
        if (_input.Attack())
        {
            // �� �� �� ��ġ�� ��� ����ó��
            if (cellpo.x == -10f)
            {
                cellpo.x = -9f;
            }
            Instantiate(_Balloon, cellpo, transform.rotation);
        }
    }


}
