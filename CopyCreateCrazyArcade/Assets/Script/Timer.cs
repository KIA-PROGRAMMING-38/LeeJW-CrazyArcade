using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private float elapsedTime;
    private bool timeCheck = true;
    public GameManager _manager;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
       
    }

    private void Update()
    {

        elapsedTime += Time.deltaTime;

        long _rT = 5 -(long)elapsedTime;
        DateTimeOffset _timer = DateTimeOffset.FromUnixTimeSeconds(_rT);

        if(timeCheck)
        _text.text = $"{_timer.Minute} : {_timer.Second}";

        if (_timer.Minute == 0 && _timer.Second <= 0)
        {
            timeCheck= false ;
            _manager.GameOver();
        }
    }
}
