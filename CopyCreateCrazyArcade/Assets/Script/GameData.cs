using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameData : MonoBehaviour
{
    public InGameSceneManager _data;
    public bool defaultMode;
    public bool monsterMode;

    public bool stage1;
    public bool stage2;
    public bool stage3;

    private void Awake()
    {
        var objs = FindObjectsOfType<GameData>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
