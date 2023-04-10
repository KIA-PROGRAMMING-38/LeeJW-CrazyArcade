using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAttack : MonoBehaviour
{
    PlayerInput _input;
    private GameObject _Balloon;

    private const float _plusPositionX = 0.42f;
    private Vector3 maxRay = new Vector3(0, 0, -1.2f);
    RaycastHit2D _raycast;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _Balloon = Resources.Load("Balloon") as GameObject;

    }

    private Vector3 selfposition;

    void Update()
    {
        // 현재 포지션을 셀포지션으로
        selfposition = MapManager.Instance.LocalToCellPosition(transform);
       
        if (_input.Attack())
        {

            // 맵 끝 에 설치할 경우 예외처리\
            
            if ( selfposition.x == -10f)
            {
                selfposition.x = -9f;
            }
            if (_raycast.collider.name != "Balloon(Clone)")
            {
                Instantiate(_Balloon, selfposition, transform.rotation);
            }

        }

    }


}
