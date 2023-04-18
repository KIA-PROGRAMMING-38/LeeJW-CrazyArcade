using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStartEvent : MonoBehaviour
{

    private Vector3 upPosition = new Vector3(0, 0.2f, 0);
    private WaitForSeconds delayTime = new WaitForSeconds(0.1f);
    public GameObject _start;
    private int childCount;
    public bool gameStart = false;
    
    private void Awake()
    {
        childCount = _start.transform.childCount;
    }
    private void Update()
    {
        StartCoroutine(StartEvent());
    }

    IEnumerator StartEvent()
    {
        yield return delayTime;

        for(int i = 0; i < childCount; i++)
        {
            _start.transform.GetChild(i).position += upPosition;
            yield return delayTime;
        }

        for(int i = 0; i < 3; ++i)
        {
            yield return delayTime;
        }


        gameObject.SetActive(false);
        gameStart= true;    
    }
}
public class GameEndEvent : MonoBehaviour
{

}
