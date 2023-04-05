using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    [Range(1f, 10f)]
    private float _speed = 3f;


    //private PlayerInput _input;
    //private Rigidbody2D _rb;

    //private void Awake()
    //{
    //    _input = GetComponent<PlayerInput>();
    //}

    float _moveSpeed;
    void Update()
    {
        _moveSpeed = _speed * Time.deltaTime;
        CharacterMovement();
    }

    // 대각선 이동을 제한하는 분기 설정
    private void CharacterMovement()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.up * _moveSpeed;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.up * _moveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * _moveSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * _moveSpeed;
        }

    }
}

