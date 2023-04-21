using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWrite : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _text;

    private void Awake()
    {
        _text[0].text = UserInput.firstUserID;
        _text[1].text = UserInput.secondUserID;
    }
}
