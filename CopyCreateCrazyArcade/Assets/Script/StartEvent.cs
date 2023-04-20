using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : MonoBehaviour
{
    public GameManager _manager;

    public void GameStart()
    {
        _manager.GameStart();
    }
}
