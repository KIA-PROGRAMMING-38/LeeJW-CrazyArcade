using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Assets.Script
{
    public class PlayerAttack : MonoBehaviour
    {
        PlayerInput _input;
        PlayerStatus _status;
        private GameObject _Balloon;
        private int attackCount;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _Balloon = Resources.Load("WaterBalloon") as GameObject;
            _status = GetComponent<PlayerStatus>();
        }

        private Vector3 selfposition;

        private bool _isBalloonOnTile = true;

        private void Update()
        {
            attackCount = _status.currentBalloonCount;

            Debug.Log(attackCount);

            // ���� �������� ������������
            if (_input.Attack() && _isBalloonOnTile)
            {
                selfposition = MapManager.Instance.LocalToCellPosition(transform);

                // �� �� �� ��ġ�� ��� ����ó��\
                if (selfposition.x > -10f)
                {
                    Vector2 spawnPosition = MapManager.Instance.LocalToCellPosition(transform);
                    Instantiate(_Balloon, spawnPosition, Quaternion.identity);
                    --attackCount;
                }

            }
        }





        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Balloon"))
            {
                _isBalloonOnTile = false;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Balloon"))
            {
                _isBalloonOnTile = true;
            }
        }

    }
}