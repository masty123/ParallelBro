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

    // Start is called before the first frame update
    void Start()
    {
        // detemine player index
        // if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient)
        // {
        //     //Get other player index


        // }
        // else
        // {
        PlayerIndex = UserData.GetInstance().GetCharacterIndex() == 1 ? 1 : 2;
        // }

        // is other player
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
            // Transform[] children = gameObject.GetComponentsInChildren<Transform>();
            // foreach (Transform child in children)
            // {
            //     child.gameObject.tag = "player";
            //     child.gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
            // }

            if (PlayerIndex == 1)
            {
                // Character2.
                Character1.SetActive(false);
                Character2.GetComponent<SpriteRenderer>().color = Color.black;
            }
            else
            {
                Character2.SetActive(false);
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
            }
            else
            {
                Character1.SetActive(false);
            }
        }

        // if (PhotonNetwork.IsConnected)
        // {
        //     PlayerIndex = PhotonNetwork.CountOfPlayersInRooms + 1;
        //     VDebug.Instance.Log(PlayerIndex.ToString());
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
