using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _input;
    PlayerStatus _status;
    private Vector3 velocity;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _status = GetComponent<PlayerStatus>();

    }

    private float _moveSpeed;

    private void Update()
    {
        if (_input._manager.gamePlay == true)
        {

            if (gameObject.name == "1PCharacter")
            {
                velocity.x = _input._1PlayerPositionX;
                velocity.y = _input._1PlayerPositionY;
            }
            if (gameObject.name == "2PCharacter")
            {
                velocity.x = _input._2PlayerPositionX;
                velocity.y = _input._2PlayerPositionY;
            }
        }
        else
        {
            if (gameObject.name == "1PCharacter")
            {
                velocity.x = 0;
                velocity.y = 0;
            }
            if (gameObject.name == "2PCharacter")
            {
                velocity.x = 0;
                velocity.y = 0;
            }
        }
    }
    void FixedUpdate()
    {

        // �밢�� �̵��� �����մϴ�.
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            velocity.y = 0;
        }
        else
        {
            velocity.x = 0;
        }
        _moveSpeed = _status.currentSpeed * Time.deltaTime;
        transform.Translate(velocity * _moveSpeed);
    }
}

