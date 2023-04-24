using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameData : MonoBehaviour
{
    public InGameSceneManager _data;
    public bool defaultMode;
    public bool monsterMode;
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

        Debug.Log($"{defaultMode} 디폴트");
        Debug.Log($"{monsterMode} 몬스터");

    }


    private void Update()
    {
        Debug.Log($"{defaultMode} 디폴트");
        Debug.Log($"{monsterMode} 몬스터");
    }
}
