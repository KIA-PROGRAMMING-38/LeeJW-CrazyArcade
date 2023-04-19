using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public GameObject _start;
    public GameObject _end;

    public bool gamePlay = false;

    private void Awake()
    {
        _end.gameObject.SetActive(false);
    }
    public void GameStart()
    {
        gamePlay = true;
        _start.SetActive(false);
    }
    public void GameOver()
    {
        gamePlay = false;
        _end.gameObject.SetActive(true);
    }

   

}
