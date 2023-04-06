using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public bool MoveUp() => Input.GetKey(KeyCode.UpArrow);
    public bool MoveDown() => Input.GetKey(KeyCode.DownArrow);
    public bool MoveLeft() => Input.GetKey(KeyCode.LeftArrow);
    public bool MoveRight() => Input.GetKey(KeyCode.RightArrow);
    public bool Attack() => Input.GetKeyDown(KeyCode.Space);

}
