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

    private bool defaultMode;
    private bool monsterMode;

    private void Awake()
    {
        _createRoom.SetActive(false);
        defaultImage.SetActive(false);
        monsterImage.SetActive(false);
        _createRoom.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void CreateRoomWindow()
    {
        _createRoom.SetActive(true);
        _createButton.SetActive(false);
    }
    public void NextScene()
    {
        if (defaultMode == false && monsterMode == false)
        {
            _createRoom.transform.GetChild(0).gameObject.SetActive(true);

        }
        if (defaultMode)
        {
            _scenesManager.FromCreateRoomToDefaultWaitRoom();
        }

        else if (monsterMode)
        {
            _scenesManager.FromCreateRoomToMonsterWaitRoom();
        }

    }

    public void PreviousWindow()
    {
        _createRoom.SetActive(false);
        _createButton.SetActive(true);
        defaultImage.SetActive(false);
        monsterImage.SetActive(false);
        defaultMode = false;
        monsterMode = false;
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
        defaultMode = true;
        monsterMode = false;
        _createRoom.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void SelectMonsterMode()
    {
        monsterImage.SetActive(true);
        defaultImage.SetActive(false);
        monsterMode = true;
        defaultMode = false;
        _createRoom.transform.GetChild(0).gameObject.SetActive(false);

    }

}
