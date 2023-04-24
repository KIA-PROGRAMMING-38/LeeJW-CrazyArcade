using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomManager : MonoBehaviour
{
    [SerializeField]
    private ScenesManager _manager;
    [SerializeField]
    private GameObject _selectMap;
    [SerializeField]
    private Button _pirateButton;
    [SerializeField]
    private Button _monsterButton;

    [SerializeField]
    private GameObject _gameRoomScene;

    private bool pirate;
    private bool monster;

    private GameData gameData;
    private void Awake()
    {
        gameData = FindObjectOfType<GameData>();

    }
    public void PreviousScene()
    {
        _manager.FromLoginToGameLobby();
    }

    public void PreviousWindowButton()
    {
        _selectMap.SetActive(false);

        _pirateButton.transform.GetChild(0).gameObject.SetActive(false);
        _pirateButton.transform.GetChild(1).gameObject.SetActive(false);
        _gameRoomScene.transform.GetChild(0).gameObject.SetActive(false);

        _gameRoomScene.transform.GetChild(1).gameObject.SetActive(true);
        _gameRoomScene.transform.GetChild(2).gameObject.SetActive(true);
        pirate = false;
        monster = false;

    }

    public void SelectMapButton()
    {
        _selectMap.SetActive(true);
        if (gameData.defaultMode)
        {
            _monsterButton.gameObject.SetActive(false);
        }
        if (gameData.monsterMode)
        {
            _pirateButton.gameObject.SetActive(false);
        }
        _gameRoomScene.transform.GetChild(1).gameObject.SetActive(false);
        _gameRoomScene.transform.GetChild(2).gameObject.SetActive(false);

    }

    public void MapSelect()
    {
        if (gameData.defaultMode)
        {
            _pirateButton.transform.GetChild(0).gameObject.SetActive(true);
            _pirateButton.transform.GetChild(1).gameObject.SetActive(true);

            pirate = true;

        }
        if (gameData.monsterMode)
        {
            _monsterButton.transform.GetChild(0).gameObject.SetActive(true);
            _monsterButton.transform.GetChild(1).gameObject.SetActive(true);

            monster = true;
        }
    }
    public void NextButton()
    {
        _gameRoomScene.transform.GetChild(1).gameObject.SetActive(true);
        _gameRoomScene.transform.GetChild(2).gameObject.SetActive(true);
        _selectMap.SetActive(false);
        if (pirate)
        {
            _gameRoomScene.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (monster)
        {
            _gameRoomScene.transform.GetChild(3).gameObject.SetActive(true);
        }
    }
    public void NextScene()
    {
        if (pirate)
            _manager.FromRoomRobbyToGamePlayScene();
        else if (monster)
            _manager.FromRoomRobbyToGamePlayScene();

    }
}
