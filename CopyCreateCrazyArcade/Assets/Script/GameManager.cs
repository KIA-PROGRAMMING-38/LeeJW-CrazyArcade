using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject _start;
    public GameObject _end;
    public GameObject firstPlayerNeedle;
    public GameObject secondPlayerNeedle;

    public bool gamePlay = false;

    private void Awake()
    {

        _end.gameObject.SetActive(false);
        firstPlayerNeedle.gameObject.SetActive(false);
        secondPlayerNeedle.gameObject.SetActive(false);

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
    public void FirstNeedle()
    {
        firstPlayerNeedle.SetActive(true);
    }
    public void SecondNeedle()
    {
        secondPlayerNeedle.SetActive(true);
    }

    public void FirstNeedleOff()
    {
        firstPlayerNeedle.SetActive(false);
    }
    public void SecondNeedleOff()
    {
        secondPlayerNeedle.SetActive(false);
    }



}
