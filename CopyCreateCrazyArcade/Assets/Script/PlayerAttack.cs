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
        // cell 사이즈에 따라 어색하게 출력되는 부분 포지션 수식으로 자연스럽게 배치.
        selfposition = transform.position;
        selfposition.x = selfposition.x + 0.42f;
        cellpo = _grid.LocalToCell(selfposition);


        Debug.Log(cellpo.x);
        if (_input.Attack())
        {
            // 맵 끝 에 설치할 경우 예외처리
            if (cellpo.x == -10f)
            {
                cellpo.x = -9f;
            }
            Instantiate(_Balloon, cellpo, transform.rotation);
        }
    }


}
