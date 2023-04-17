using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float _1PlayerPositionX { get; private set; }
    public float _1PlayerPositionY { get; private set; }

    public float _2PlayerPositionX { get; private set; }
    public float _2PlayerPositionY { get; private set; }


    private void Update()
    {
        _1PlayerPositionX = Input.GetAxisRaw("Horizontal");
        _1PlayerPositionY = Input.GetAxisRaw("Vertical");

        _2PlayerPositionX = Input.GetAxisRaw("SecondHorizontal");
        _2PlayerPositionY = Input.GetAxisRaw("SecondVertical");


    }
    public bool FirstPlayerAttack() => Input.GetKeyDown(KeyCode.Space);
    public bool SecondPlayerAttack() => Input.GetKeyDown(KeyCode.LeftShift);


}
