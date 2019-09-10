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
    private string username;
    public Text UserNameText;
    public InputField RoomCodeText;

    public byte maxPlayersPerRoom = 2;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // try to connect the master server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master!!");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby!!!");
        // Change to auth;
        username = GetRandomName();
        // set game nickname
        PhotonNetwork.NickName = username;
        UserNameText.text = username;
        Debug.Log("Name: " + username);

        // switch screen
        LobbyScreen.SetActive(true);
        LoadingScreen.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joining");
        PhotonNetwork.LoadLevel("guytestscene");
    }

    #region UIMethods
    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomCodeText.text);
    }

    public void OnClick_CreateRoom()
    {
        // random room code
        string roomName = GetRandomRoomName();
        Debug.Log("Created : #Roomname " + roomName);
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, TypedLobby.Default);
    }
    #endregion

    // use for testing as we don't have auth.
    private string[] names = new string[] { "Peter", "Ron", "Satchmo", "Ayesha", "Jenkins", "Margaret", "Summers", "Anita", "Finley", "Savannah", "Mcintosh" };
    public string GetRandomName()
    {
        return names[Random.Range(0, names.Length)];
    }

    private string GetRandomRoomName()
    {
        return Random.Range(100000, 999999).ToString();
    }
}
