using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public GameObject _start;
    public GameObject _end;
    public GameObject firstPlayerNeedle;
    public GameObject secondPlayerNeedle;
    public AudioSource _audio;
    public ScenesManager _scene;
    public int playerCount = 2;
    private GameData _gameData;
    private GameObject pirateMap;
    private GameObject monsterStage1;
    public bool gamePlay = false;

    private WaitForSeconds changeTime = new WaitForSeconds(9f);

    private void Awake()
    {
        _gameData = FindAnyObjectByType<GameData>();

        _end.gameObject.SetActive(false);
        firstPlayerNeedle.gameObject.SetActive(false);
        secondPlayerNeedle.gameObject.SetActive(false);

        pirateMap = Resources.Load("PirateBlocks") as GameObject;
        monsterStage1 = Resources.Load("MonsterStage1Blocks") as GameObject;


        if (_gameData.defaultMode)
        {
            Instantiate(pirateMap, transform.position, transform.rotation);
        }
        if (_gameData.monsterMode)
        {
            Instantiate(monsterStage1);
        }
    }

    public void GameStart()
    {
        gamePlay = true;
        _start.SetActive(false);
    }
    public void GameOver()
    {
        if (_gameData.defaultMode)
        {
            gamePlay = false;
            _end.gameObject.SetActive(true);
            _audio.Stop();
            StartCoroutine(SceneChange());
        }

        if(_gameData.monsterMode && playerCount == 0)
        {
            gamePlay = false;
            _end.gameObject.SetActive(true);
            _audio.Stop();
            StartCoroutine(SceneChange());
        }
    }

    public void FirstNeedle()
    {
        firstPlayerNeedle.SetActive(true);
    }
    public void SecondNeedle()
    {
        secondPlayerNeedle.SetActive(true);
    }

    public void FirstNeedleOff()
    {
        firstPlayerNeedle.SetActive(false);
    }
    public void SecondNeedleOff()
    {
        secondPlayerNeedle.SetActive(false);
    }

    IEnumerator SceneChange()
    {
        yield return changeTime;
        _scene.FromCreateRoomToDefaultWaitRoom();
    }
    public void PlayerDie()
    {
        playerCount--;
    }

}
