using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _createRoom;
    [SerializeField]
    private ScenesManager _scenesManager;
    [SerializeField]
    private GameObject _createButton;
    [SerializeField]
    private GameObject defaultImage;
    [SerializeField]
    private GameObject monsterImage;
    private GameData gameData;



    private void Awake()
    {
        _createRoom.SetActive(false);
        defaultImage.SetActive(false);
        monsterImage.SetActive(false);
        _createRoom.transform.GetChild(0).gameObject.SetActive(false);


        gameData = FindObjectOfType<GameData>();
        gameData.defaultMode = false;
        gameData.monsterMode = false;
    }

    public void CreateRoomWindow()
    {
        _createRoom.SetActive(true);
        _createButton.SetActive(false);
    }
    public void NextScene()
    {
        if (gameData.defaultMode == false && gameData.monsterMode == false)
        {
            _createRoom.transform.GetChild(0).gameObject.SetActive(true);

        }
        if (gameData.defaultMode)
        {
            _scenesManager.FromCreateRoomToDefaultWaitRoom();
        }

        else if (gameData.monsterMode)
        {
            _scenesManager.FromCreateRoomToDefaultWaitRoom();
        }

    }

    public void PreviousWindow()
    {
        _createRoom.SetActive(false);
        _createButton.SetActive(true);
        defaultImage.SetActive(false);
        monsterImage.SetActive(false);
        gameData.defaultMode = false;
        gameData.monsterMode = false;
        _createRoom.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void PreviousScene()
    {
        _scenesManager.FromGameLobbyToLogin();
    }

    public void SelectDefaultMode()
    {
        defaultImage.SetActive(true);
        monsterImage.SetActive(false);
        gameData.defaultMode = true;
        gameData.monsterMode = false;
        _createRoom.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void SelectMonsterMode()
    {
        monsterImage.SetActive(true);
        defaultImage.SetActive(false);
        gameData.monsterMode = true;
        gameData.defaultMode = false;
        _createRoom.transform.GetChild(0).gameObject.SetActive(false);

    }

}
