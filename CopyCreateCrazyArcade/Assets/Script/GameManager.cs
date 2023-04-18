using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject _start;
    public GameObject _end;

    private Vector3 upPosition = new Vector3(0, 0.2f, 0);
    private WaitForSeconds delayTime = new WaitForSeconds(0.1f);
    private WaitForSeconds exitTime = new WaitForSeconds(0.3f);
    public bool gameStart = false;
    public bool gameEnd = false;

    private int startChildCount;

    private void Awake()
    {
        startChildCount = _start.transform.childCount;
        _end.gameObject.SetActive(false);
    }
    void Update()
    {
        StartCoroutine(StartEvent());

       
    }

   public void GameOver()
    {
        _end.gameObject.SetActive(true);
    }
    IEnumerator StartEvent()
    {
        yield return delayTime;

        for(int i = 0; i < startChildCount; i++)
        {
            _start.transform.GetChild(i).position += upPosition;
            yield return delayTime;
        }

       yield return exitTime;


        _start.gameObject.SetActive(false);
        gameStart= true;    
    }
}
