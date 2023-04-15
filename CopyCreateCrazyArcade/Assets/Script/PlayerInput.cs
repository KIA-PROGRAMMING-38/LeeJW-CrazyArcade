using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float _horizontal;
    public float _vertical;

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
   
        _vertical =  Input.GetAxisRaw("Vertical");

    }
    public bool Attack() => Input.GetKeyDown(KeyCode.Space);

}
