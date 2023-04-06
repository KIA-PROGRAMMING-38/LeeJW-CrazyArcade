using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _input;

    [SerializeField]
    [Range(1f, 10f)]
    private float _speed = 3f;


    private void Awake()
    {
        _input = GetComponent<PlayerInput>();   
    }

    float _moveSpeed;
    void Update()
    {
        _moveSpeed = _speed * Time.deltaTime;

        CharacterMovement();
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

