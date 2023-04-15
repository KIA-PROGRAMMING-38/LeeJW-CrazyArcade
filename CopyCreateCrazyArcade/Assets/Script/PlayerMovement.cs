using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _input;
    Rigidbody2D _rigidbody;
    PlayerStatus _status;
    private Vector3 velocity;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _status = GetComponent<PlayerStatus>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private float _moveSpeed;

    private void Update()
    {
        // 대각선 제한
        velocity.x = _input._horizontal;
        velocity.y = _input._vertical;
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

