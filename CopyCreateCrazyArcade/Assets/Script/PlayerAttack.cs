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
        transform.SetParent(null);
        _input = GetComponent<PlayerInput>();
        _Balloon = Resources.Load("Balloon") as GameObject;
    }

    private Vector3 selfposition;

    private bool _isBalloonOnTile = true;

    void Update()
    {

        // 현재 포지션을 셀포지션으로
        selfposition = MapManager.Instance.LocalToCellPosition(transform);


        if (_input.Attack() && _isBalloonOnTile)
        {
            
            // 맵 끝 에 설치할 경우 예외처리\

            if (selfposition.x == -10f)
            {
                selfposition.x = -9f;
            }

            Instantiate(_Balloon, selfposition, transform.rotation);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Attack"))
        {
            _isBalloonOnTile = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Attack"))
        {
            _isBalloonOnTile = true;
        }
    }

}
