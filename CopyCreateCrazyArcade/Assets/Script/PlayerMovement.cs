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

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();   
        _status = GetComponent<PlayerStatus>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private float _moveSpeed;

    void Update()
    {
        _moveSpeed = _status.currentSpeed * Time.deltaTime;

        CharacterMovement();


    }
    private void FixedUpdate()
    {

    }

    // �밢�� �̵��� �����ϴ� �б� ����
    private void CharacterMovement()
    {
        if (_input.MoveDown())
        {
            transform.position -= transform.up * _moveSpeed;
        }
        else if (_input.MoveUp())
        {
            transform.position += transform.up * _moveSpeed;
        }
        else if (_input.MoveLeft())
        {
            transform.position -= transform.right * _moveSpeed;
        }
        else if (_input.MoveRight())
        {
            transform.position += transform.right * _moveSpeed;
        }

    }
}

