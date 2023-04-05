using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    [Range(1f, 10f)]
    private float _speed = 3f;

    float _moveSpeed;

    void Update()
    {
        _moveSpeed = _speed * Time.deltaTime;
        CharacterMovement();
    }

    // �밢�� �̵��� �����ϴ� �б� ����
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

