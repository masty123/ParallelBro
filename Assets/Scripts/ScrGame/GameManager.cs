using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Text RoomCodeText;

    public GameObject playerPrefab;
    public Button startButton;
    [HideInInspector]
    public double startTime;
    public Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            if (RoomCodeText != null)
                RoomCodeText.text = "Offline Mode";
        }
        else
        {
            if (RoomCodeText != null)
                RoomCodeText.text = PhotonNetwork.CurrentRoom.Name;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                Debug.Log(player.NickName);
            }
        }
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient)
        {
            if (startButton)
                startButton.gameObject.SetActive(false);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            GameObject myPlayer = null;
            GameObject[] players = GameObject.FindGameObjectsWithTag("player");
            foreach (GameObject player in players)
            {
                if (player.GetComponent<PhotonView>().IsMine)
                {
                    myPlayer = player;
                }
            }
            myPlayer.GetComponent<PhotonView>().RPC("SetTimer", RpcTarget.AllBuffered, PhotonNetwork.Time);
        }
        playerIndex();
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // instancate player prefab
        PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, Quaternion.identity, 0);
    }

    private void FixedUpdate()
    {
        int ActualTime = Mathf.RoundToInt((float)(PhotonNetwork.Time - startTime));
        int second = ActualTime % 60;
        int minute = ActualTime / 60;
        TimerText.text = minute.ToString() + ":" + second.ToString("00");
    }

    public void playerIndex()
    {
        GameObject myPlayer = null;
        GameObject otherPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
            else
            {
                otherPlayer = player;
            }
        }
        // detemine player index
        // if host
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            int HostPlayerIndex = UserData.GetInstance().GetCharacterIndex();
            myPlayer.GetComponent<PhotonView>().RPC("SetAll", RpcTarget.AllBuffered, HostPlayerIndex);
            Debug.Log(otherPlayer);
            if (otherPlayer == null)
            {
                return;
            }
            if (HostPlayerIndex == 1)
            {
                otherPlayer.GetComponent<PhotonView>().RPC("SetAll", RpcTarget.AllBuffered, 2);
            }
            else
            {
                otherPlayer.GetComponent<PhotonView>().RPC("SetAll", RpcTarget.AllBuffered, 1);
            }
        }
    }

    public void StartGame()
    {
        // LoadArena();
        // PhotonNetwork.LoadLevel("prototype");
        PhotonNetwork.LoadLevel("level" + RoomData.GetInstance().level);
    }

    public void Ready()
    {
        GameObject myPlayer = null;
        GameObject otherPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
            else
            {
                otherPlayer = player;
            }
        }

        myPlayer.GetComponent<PlayerAction>().Ready();
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        // Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("player");
            foreach (GameObject go in gos)
            {
                if (go.GetComponent<PhotonView>().IsMine)
                {
                    go.GetComponent<PlayerAction>().ChangeLevel(RoomData.GetInstance().roomName, RoomData.GetInstance().level);
                }
            }
        }
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
