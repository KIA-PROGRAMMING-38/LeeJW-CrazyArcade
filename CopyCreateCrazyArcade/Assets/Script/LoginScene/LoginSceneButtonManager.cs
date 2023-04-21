using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSceneButtonManager : MonoBehaviour
{
    [SerializeField]
    private ScenesManager _scenesmanager;
    [SerializeField]
    private GameObject _failLoginWindow;

    private void Awake()
    {
        _failLoginWindow.SetActive(false);
    }
    public void ClosefailLoginWindow()
    {

        _failLoginWindow.SetActive(false);
    }
    public void NextScene()
    {
        if (UserInput.firstUserID != null && UserInput.secondUserID != null)
        {
            _scenesmanager.FromLoginToGameLobby();
        }
        if(UserInput.firstUserID == null || UserInput.secondUserID == null)
        {
            _failLoginWindow.SetActive(true);
        }
    }
}
