using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SecondPlayerMovement : MonoBehaviour
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
        // 대각선 제한
        velocity.x = _input._2PlayerPositionX;
        velocity.y = _input._2PlayerPositionY;
    }
    void FixedUpdate()
    {

        // 대각선 이동을 방지합니다.
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

