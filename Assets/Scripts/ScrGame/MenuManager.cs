using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public GameObject LoadingScreen;
    public GameObject LobbyScreen;
    public GameObject LoginScreen;
    public GameObject LevelScreen;
    public GameObject CharacterScreen;
    public GameObject JoinScreen;
    public GameObject titleText;
    public Text UserNameText;
    public InputField RoomCodeText;

    private RoomData roomData;
    /* 
    * work flow
    *
    * Open App -> Load Login Screen , if already logged in , skip
    * Logged in -> Connect to server, Load Lobby screen 
    * 
    * 
    * Join / Select Character = Create Room
    */

    private void Awake()
    {
        bool isLoggedIn = false;

        if (!isLoggedIn)
        {
            LoginScreen.SetActive(true);
        }
        else
        {
            LobbyScreen.SetActive(true);
            HandleLobbyScreen();
        }

        // connect to server
        PhotonNetwork.ConnectUsingSettings();
    }

    #region Login
    // Handle Login Screen
    public void OnClickLoginButton()
    {
        StartCoroutine(WaitForAnimationLogin());
    }

    private void handleFacebookLogin()
    {
        bool Success = true;
        if (Success)
        {
            UserData.GetInstance().SetUsername(GetRandomName());
            PhotonNetwork.NickName = UserData.GetInstance().GetUsername();
            LoginScreen.SetActive(false);
            HandleLobbyScreen();
        }

        // error handler
    }
    #endregion

    #region Lobby
    // Handle Lobby Screen
    private void HandleLobbyScreen()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            // loading scene until ready
            LoadingScreen.SetActive(true);
        }
        else
        {
            LobbyScreen.SetActive(true);
            // set username
            UserNameText.text = PhotonNetwork.NickName;
            // set userimage if needed
        }
    }

    public void OnClickCreateRoom()
    {
        StartCoroutine(WaitForAnimationCreateButton());
    }

    public void OnClickJoinRoom()
    {
        LobbyScreen.SetActive(false);
        JoinScreen.SetActive(true);
    }

    public void OnClickSetting()
    {
        Debug.Log("Setting clicked");
        // do not thing.
    }
    #endregion

    #region Level
    // Handle Level Screen
    public void OnClickLevel(int level)
    {
       
        StartCoroutine( WaitForAnimationOnClickLevel(level));
    
    }

    public void OnClickBackLevel()
    {
        StartCoroutine(WaitForAnimationOnClickLevelBack());
    }
    #endregion

    #region Character
    // Handle Character Screen
    public void OnClickCharacter(int characterIndex)
    {
        UserData.GetInstance().SetCharacterIndex(characterIndex);
        // Load player waitting room scene
        string roomName = roomData.roomName;
        Debug.Log("Created : #Roomname " + roomName);
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default);
    }

    public void OnClickBackCharacter()
    {
       StartCoroutine(WaitForAnimationBackCharacter());
    
    }
    #endregion

    #region Join
    // Handle Join Screen
    public void OnClickClear()
    {
        RoomCodeText.text = "";
    }

    public void OnClickJoin()
    {
        PhotonNetwork.JoinRoom(RoomCodeText.text);
    }

    public void OnClickBackJoin()
    {
        JoinScreen.SetActive(false);
        LobbyScreen.SetActive(true);
    }
    #endregion

    public byte maxPlayersPerRoom = 2;

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master!!");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        if (LoadingScreen.activeSelf)
        {
            LoadingScreen.SetActive(false);
            LobbyScreen.SetActive(true);
        }
    }

    public override void OnJoinedRoom()
    {
        // PhotonNetwork.LoadLevel("guytestscene");

        // PhotonNetwork.LoadLevel("interactabletestscene");
        PhotonNetwork.LoadLevel("waitingscene");
    }

    // // use for testing as we don't have auth.
    private string[] names = new string[] { "Peter", "Ron", "Satchmo", "Ayesha", "Jenkins", "Margaret", "Summers", "Anita", "Finley", "Savannah", "Mcintosh" };
    public string GetRandomName()
    {
        return names[Random.Range(0, names.Length)];
    }


    IEnumerator WaitForAnimationLogin()
    {
        yield return new WaitForSeconds(1f);
        handleFacebookLogin();
    }

    IEnumerator WaitForAnimationCreateButton()
    {
        yield return new WaitForSeconds(1.2f);
        LobbyScreen.SetActive(false);
        LevelScreen.SetActive(true);
        titleText.SetActive(false);

    }

    IEnumerator WaitForAnimationSettingButton()
    {
        yield return new WaitForSeconds(1.2f);
        OnClickSetting();

    }

    IEnumerator WaitForAnimationBackButton()
    {
        yield return new WaitForSeconds(1.2f);
    }

    IEnumerator WaitForAnimationBackCharacter()
    {
        yield return new WaitForSeconds(1f);
        roomData = null;
        CharacterScreen.SetActive(false);
        LevelScreen.SetActive(true);
    }


    IEnumerator WaitForAnimationOnClickLevel(int level)
    {
        yield return new WaitForSeconds(1f);
        roomData = new RoomData(level);
        LevelScreen.SetActive(false);
        CharacterScreen.SetActive(true);
    }

    IEnumerator WaitForAnimationOnClickLevelBack()
    {
        yield return new WaitForSeconds(.8f);
        LevelScreen.SetActive(false);
        LobbyScreen.SetActive(true);
    }




}

class RoomData
{
    public int level;
    public string roomName;

    public RoomData(int level)
    {
        this.level = level;
        roomName = level + GetRandomRoomName();
    }

    private string GetRandomRoomName()
    {
        return Random.Range(10000, 99999).ToString();
    }
}