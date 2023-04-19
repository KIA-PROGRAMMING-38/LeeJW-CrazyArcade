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
    private int startChildCount;
    Coroutine _coro;

    private void Awake()
    {
        startChildCount = _start.transform.childCount;
        _end.gameObject.SetActive(false);


    }
    void Update()
    {
        _coro = StartCoroutine(StartEvent());
        if (_start.transform.GetChild(5).position.y >= endPosition.y)
        {
            gamePlay = true;
            _start.gameObject.SetActive(false);
            StopCoroutine(_coro);
            
        }
    }

    public void GameOver()
    {
        gamePlay = false;
        _end.gameObject.SetActive(true);
    }

    IEnumerator StartEvent()
    {
        yield return delayTime;

        for (int i = 0; i < startChildCount; ++i)
        {
            _start.transform.GetChild(i).position += upPosition;

            yield return delayTime;
        }


    }

}
