using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkOwnerShip : MonoBehaviourPun
{
    [Header("Player 1 or Player 2")]
    public int PlayerIndex = 0;
    public GameObject Character1;
    public GameObject Character2;

    private void Awake()
    {
        if (UserData.GetInstance().GetCharacterIndex() == 0)
        {
            GetComponent<PhotonView>().RPC("RequestPlayerIndex", RpcTarget.AllBuffered);
        }
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     // // detemine player index
    //     // // if host
    //     // if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
    //     // {
    //     //     PlayerIndex = UserData.GetInstance().GetCharacterIndex();
    //     //     GetComponent<PhotonView>().RPC("SetAll", RpcTarget.AllBuffered, PlayerIndex);
    //     // }
    //     // else
    //     // {
    //     //     // Find other player
    //     //     GameObject myPlayer = null;
    //     //     int myPlayerIndex = 0;
    //     //     GameObject[] players = GameObject.FindGameObjectsWithTag("player");
    //     //     foreach (GameObject player in players)
    //     //     {
    //     //         if (player.GetComponent<PhotonView>().IsMine)
    //     //         {
    //     //             myPlayer = player;
    //     //             continue;
    //     //         }
    //     //         int index = player.GetComponent<NetworkOwnerShip>().PlayerIndex;
    //     //         PlayerIndex = index;
    //     //         if (index == 1)
    //     //         {
    //     //             myPlayerIndex = 2;
    //     //         }
    //     //         else if (index == 2)
    //     //         {
    //     //             myPlayerIndex = 1;
    //     //         }
    //     //         checkCharacterSprite();
    //     //     }
    //     //     myPlayer.GetComponent<NetworkOwnerShip>().SetPlayerIndex(myPlayerIndex);
    //     // }
    //     checkCharacterSprite();
    // }

    public void SetPlayerIndex(int playerIndex)
    {
        this.PlayerIndex = playerIndex;
        checkCharacterSprite();
    }


    private void checkCharacterSprite()
    {
        // is other player
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
            if (PlayerIndex == 2)
            {
                // Character2.
                Character1.SetActive(false);
                Character2.SetActive(true);
                Character2.GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                Character2.SetActive(false);
                Character1.SetActive(true);
                Character1.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
        // is my own player
        else
        {
            // attach to camera
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().Player = this.gameObject;
            if (PlayerIndex == 1)
            {
                Character2.SetActive(false);
                Character1.SetActive(true);
            }
            else
            {
                Character1.SetActive(false);
                Character2.SetActive(true);
            }
        }
    }
}
