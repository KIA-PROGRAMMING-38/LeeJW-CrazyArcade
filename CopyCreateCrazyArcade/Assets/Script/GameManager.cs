using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public GameObject _start;
    public GameObject _endWin;
    public GameObject _endLose;
    public GameObject firstPlayerNeedle;
    public GameObject secondPlayerNeedle;
    public AudioSource _audio;
    public ScenesManager _scene;
    public Timer timer;
    public GameObject _gameStart;
    public int playerCount = 2;
    public int monsterCount = 6;
    public Camera _camera;
    private int stage2MonsterCount = 8;

    private GameData _gameData;

    private GameObject pirateMap;
    private GameObject monsterStage1;
    private GameObject monsterStage2;
    private GameObject monsterStage3;

    private GameObject firstPlayer;
    private GameObject secondPlayer;
    private Vector3[] firstPosition = new Vector3[3];
    private Vector3[] secondPosition = new Vector3[3];
    private Vector3 monsterModeFirstPosition = new Vector3(-7, 1.5f, 0);
    private Vector3 monsterModeSecondPosition = new Vector3(3, 1.5f, 0);
    private Vector3 stage2FirstPosition = new Vector3(-3, 0.5f, 0);
    private Vector3 stage2SecondPosition = new Vector3(-1, 2.5f, 0);
    private Vector3 stage3FirstPosition = new Vector3(-6, -3.5f, 0);
    private Vector3 stage3SecondPosition = new Vector3(1, -3.5f, 0);

    private int firstRandom;
    private int secondRandom;

    private GameObject stage;
    private GameObject firstSave;
    private GameObject secondSave;

    public bool gamePlay = false;
    public bool playerLive = true;

    private WaitForSeconds changeTime = new WaitForSeconds(9f);
    private WaitForSeconds cameraOffitime = new WaitForSeconds(1f);

    private void Awake()
    {

        _gameData = FindObjectOfType<GameData>();


        _endWin.gameObject.SetActive(false);
        _endLose.gameObject.SetActive(false);

        firstPlayerNeedle.gameObject.SetActive(false);
        secondPlayerNeedle.gameObject.SetActive(false);

        pirateMap = Resources.Load("PirateBlocks") as GameObject;
        firstPlayer = Resources.Load("1PCharacter") as GameObject;
        secondPlayer = Resources.Load("2PCharacter") as GameObject;

        monsterStage1 = Resources.Load("MonsterStage1Blocks") as GameObject;
        monsterStage2 = Resources.Load("MonsterStage2Blocks") as GameObject;
        monsterStage3 = Resources.Load("MonsterStage3Blocks") as GameObject;



        firstRandom = UnityEngine.Random.Range(0, 3);
        secondRandom = UnityEngine.Random.Range(0, 3);

        firstPosition[0] = new Vector3(-4, 7.5f, 0);
        firstPosition[1] = new Vector3(-5, 0.5f, 0);
        firstPosition[2] = new Vector3(-7, -4.5f, 0);

        secondPosition[0] = new Vector3(0, 7.5f, 0);
        secondPosition[1] = new Vector3(1, 0.5f, 0);
        secondPosition[2] = new Vector3(3, -4.5f, 0);


        if (_gameData.defaultMode)
        {
            Instantiate(pirateMap, transform.position, transform.rotation);
            firstSave = Instantiate(firstPlayer, firstPosition[firstRandom], transform.rotation);
            secondSave = Instantiate(secondPlayer, secondPosition[secondRandom], transform.rotation);

        }

        if (_gameData.monsterMode)
        {
            _gameData.stage1 = true;
            stage = Instantiate(monsterStage1);
            firstSave = Instantiate(firstPlayer, monsterModeFirstPosition, transform.rotation);
            secondSave = Instantiate(secondPlayer, monsterModeSecondPosition, transform.rotation);

        }
    }
    private void Update()
    {

        if (_gameData.stage1 && monsterCount == 0)
        {
            monsterCount = stage2MonsterCount;

            StartCoroutine(IsStageOne());


        }


        if (_gameData.stage2 && monsterCount == 0)
        {
            monsterCount = 1;

            StartCoroutine(IsStageTwo());

        }
        if (_gameData.stage3 && monsterCount == 0)
        {
            playerLive = false;
            GameOver();

        }
        if (playerCount == 0)
        {
            elap += Time.deltaTime;
            if (elap > 2.5f)
            {
                GameLose();
            }
        }


    }
    private float elap;
    IEnumerator IsStageOne()
    {
        _camera.gameObject.SetActive(false);
        gamePlay = false;
        yield return cameraOffitime;
        _camera.gameObject.SetActive(true);
        gamePlay = true;


        _gameStart.SetActive(true);
        timer.elapsedTime = 0;
        playerCount = 2;
        _gameData.stage1 = false;
        _gameData.stage2 = true;

        stage.SetActive(false);
        firstSave.SetActive(false);
        secondSave.SetActive(false);
        stage = Instantiate(monsterStage2);
        firstSave = Instantiate(firstPlayer, stage2FirstPosition, transform.rotation);
        secondSave = Instantiate(secondPlayer, stage2SecondPosition, transform.rotation);

    }
    IEnumerator IsStageTwo()
    {
        _camera.gameObject.SetActive(false);
        gamePlay = false;

        yield return cameraOffitime;
        _camera.gameObject.SetActive(true);
        gamePlay = true;


        _gameStart.SetActive(true);
        timer.elapsedTime = 0;
        playerCount = 2;
        _gameData.stage2 = false;
        _gameData.stage3 = true;

        stage.SetActive(false);
        firstSave.SetActive(false);
        secondSave.SetActive(false);

        Instantiate(monsterStage3);
        firstSave = Instantiate(firstPlayer, stage3FirstPosition, transform.rotation);
        secondSave = Instantiate(secondPlayer, stage3SecondPosition, transform.rotation);
    }

    public void GameStart()
    {
        gamePlay = true;
        _start.SetActive(false);
    }
    public void GameOver()
    {
        timer.timeCheck = false;
        gamePlay = false;
        _endWin.gameObject.SetActive(true);
        _audio.Stop();
        StartCoroutine(SceneChange());
    }
    public void GameLose()
    {
        timer.timeCheck = false;
        gamePlay = false;
        _endLose.gameObject.SetActive(true);
        _audio.Stop();
        StartCoroutine(SceneChange());
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
