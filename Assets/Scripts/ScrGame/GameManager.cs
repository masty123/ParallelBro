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
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            RoomCodeText.text = "Offline Mode";
        }
        else
        {
            RoomCodeText.text = PhotonNetwork.CurrentRoom.Name;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                Debug.Log(player.NickName);
            }
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

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                // LoadArena();
                PhotonNetwork.LoadLevel("prototype");
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
    }
}
