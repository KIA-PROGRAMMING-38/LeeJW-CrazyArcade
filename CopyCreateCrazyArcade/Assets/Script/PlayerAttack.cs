using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAttack : MonoBehaviour
{
    PlayerInput _input;
    private GameObject _Balloon;
    public Grid _grid;
    private const float _plusPositionX = 0.42f;
    private Vector3 maxRay = new Vector3(0, 0, -1.2f);
    RaycastHit2D _raycast;
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _Balloon = Resources.Load("Balloon") as GameObject;
        _grid = GetComponent<Grid>();
        //_raycast = GetComponent<RaycastHit2D>();

    }
    Vector3 cellpo;
    Vector3 selfposition;
    private void FixedUpdate()
    {

        Debug.DrawLine(transform.position + new Vector3(0, 0, -0.5f), transform.position + maxRay, Color.blue);
        _raycast = Physics2D.Raycast(transform.position, transform.position + maxRay);
        if (_raycast.collider != null)
        {
            Debug.Log($"{_raycast.collider.name}");
        }
        else
        {
            Debug.Log("널값임");
        }
    }
    void Update()
    {
        // cell 사이즈에 따라 어색하게 출력되는 부분 포지션 수식으로 자연스럽게 배치.
        selfposition = transform.position;
        selfposition.x = selfposition.x + _plusPositionX;
        cellpo = _grid.LocalToCell(selfposition);

        if (_input.Attack())
        {

            // 맵 끝 에 설치할 경우 예외처리
            if (cellpo.x == -10f)
            {
                cellpo.x = -9f;
            }
            if (_raycast.collider.name != "Balloon(Clone)")
            {
                Instantiate(_Balloon, cellpo, transform.rotation);


            }



        }

    }


}
