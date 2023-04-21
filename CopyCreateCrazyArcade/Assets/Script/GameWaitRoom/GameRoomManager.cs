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
    private GameObject _gameRoomScene;

    private bool pirate;

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

    }
    public void SelectMapButton()
    {
        _selectMap.SetActive(true);
        _gameRoomScene.transform.GetChild(1).gameObject.SetActive(false);
        _gameRoomScene.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void PirateMapSelect()
    {
        _pirateButton.transform.GetChild(0).gameObject.SetActive(true);
        _pirateButton.transform.GetChild(1).gameObject.SetActive(true);

        pirate = true;
    }
    public void NextButton()
    {
        _gameRoomScene.transform.GetChild(1).gameObject.SetActive(true);
        _gameRoomScene.transform.GetChild(2).gameObject.SetActive(true);
        if (pirate)
        {
            _selectMap.SetActive(false);
            _gameRoomScene.transform.GetChild(0).gameObject.SetActive(true);

        }
    }
    public void NextScene()
    {
        if(pirate)
        _manager.FromRoomRobbyToGamePlayScene();
    }
}
