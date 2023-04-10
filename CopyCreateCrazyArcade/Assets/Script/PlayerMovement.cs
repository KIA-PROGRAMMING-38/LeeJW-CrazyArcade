using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _input;

    PlayerStatus _status;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();   
        _status = GetComponent<PlayerStatus>();
    }
    
    private float _moveSpeed;

    void Update()
    {
        _moveSpeed = _status.currentSpeed * Time.deltaTime;
        
        CharacterMovement();
    }

    // 대각선 이동을 제한하는 분기 설정
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

