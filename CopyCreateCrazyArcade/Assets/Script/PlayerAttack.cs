using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerInput _input;
    private GameObject _Balloon;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _Balloon = Resources.Load("Balloon") as GameObject;
    }

    void Update()
    {
        if (_input.Attack())
        {

            Debug.Log(123);
            Instantiate(_Balloon, transform.position, transform.rotation);
        }
    }
}
