using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public GameObject UserNameScreen, ConnectScreen;

    public GameObject CreateUserNameButton;

    public InputField UserNameInput, RoomNameInput;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
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
        UserNameScreen.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined");
        // base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(1);
    }

    #region UIMethods
    public void OnClick_CreateNameBtn()
    {
        if (UserNameInput.text.Length >= 2)
        {
            PhotonNetwork.NickName = UserNameInput.text;
            UserNameScreen.SetActive(false);
            ConnectScreen.SetActive(true);
        }
    }

    public void OnClick_JoinRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(RoomNameInput.text, ro, TypedLobby.Default);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
