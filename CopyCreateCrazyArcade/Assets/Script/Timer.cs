using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public float elapsedTime;
    public bool timeCheck = true;
    public GameManager _manager;

    private DateTimeOffset _timer;
    private long _threeminute;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _threeminute = 180;
    }

    private void Update()
    {


        if (timeCheck)
        {
            elapsedTime += Time.deltaTime;
            _timer = DateTimeOffset.FromUnixTimeSeconds(_threeminute - (long)elapsedTime);
        }
        _text.text = $"{_timer.Minute} : {_timer.Second}";

        if (_timer.Minute == 0 && _timer.Second <= 0)
        {
            timeCheck = false;
            _manager.GameOver();
        }

    }
}
