using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public  class UserInput : MonoBehaviour
{
    public TMP_InputField[] _InputID;

    public static string firstUserID;
    public static string secondUserID;


    public void Awake()
    {
        var objs = FindObjectsOfType<UserInput>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void FirstUserInputID()
    {
        firstUserID = _InputID[0].text;
    }
    public void SecondUserInputID()
    {
        secondUserID = _InputID[1].text;
    }
    
    

}
