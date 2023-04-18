using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartEvent : MonoBehaviour
{
    private Vector3 upPosition = new Vector3(0, 0.2f, 0);
    private WaitForSeconds delayTime = new WaitForSeconds(0.1f);
    public bool gameStart = false;
    private void Update()
    {
        StartCoroutine(StartEvent());
    }

    IEnumerator StartEvent()
    {
        yield return delayTime;
        transform.GetChild(0).position += upPosition;
        yield return delayTime;

        transform.GetChild(1).position += upPosition;
        yield return delayTime;

        transform.GetChild(2).position += upPosition;
        yield return delayTime;

        transform.GetChild(3).position += upPosition;
        yield return delayTime;

        transform.GetChild(4).position += upPosition;
        yield return delayTime;

        transform.GetChild(5).position += upPosition;
        yield return delayTime;
        yield return delayTime;
        yield return delayTime;


        gameObject.SetActive(false);
        gameStart= true;    
    }
}
