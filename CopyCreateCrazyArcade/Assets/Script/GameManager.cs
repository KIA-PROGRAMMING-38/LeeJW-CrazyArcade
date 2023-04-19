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

    private Vector3 upPosition = new Vector3(0, 0.2f, 0);
    private Vector3 endPosition = new Vector3(0, 10f, 0);
    private WaitForSeconds delayTime = new WaitForSeconds(0.05f);
    public bool gamePlay = false;

    private void Awake()
    {
        _end.gameObject.SetActive(false);
    }
    void Update()
    {
        Debug.Log(gamePlay);
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
