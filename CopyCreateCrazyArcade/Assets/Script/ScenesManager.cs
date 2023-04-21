using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Assets.Script
{
    public class ScenesManager : MonoBehaviour
    {
        public void FromLoginToGameLobby()
        {
            SceneManager.LoadScene(NameOfScenes.IngameLobby);
        }
        public void FromLobbyToCreateRoom()
        {
            SceneManager.LoadScene(NameOfScenes.CreateRoom);
        }
        public void FromCreateRoomToDefaultWaitRoom()
        {
            SceneManager.LoadScene(NameOfScenes.DefaultWaitRoom);
        }
        public void FromCreateRoomToMonsterWaitRoom()
        {

        }
        public void FromRoomRobbyToGamePlayScene()
        {
            SceneManager.LoadScene(NameOfScenes.GamePlay);
        }
        public void FromGameLobbyToLogin()
        {
            SceneManager.LoadScene(NameOfScenes.Login);
        }
    

    }
}
