using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    void Start()
    {
        
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
       

    }
}
