using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkOwnerShip : MonoBehaviourPun
{
    [Header("Player 1 or Player 2")]
    public int PlayerIndex = 0;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // is other player
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            spriteRenderer.color = Color.black;
            Debug.Log("Color Changed");
            gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
            Transform[] children = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                child.gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
            }
        }
        // is my own player
        else
        {
            // attach to camera
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().Player = this.gameObject;
        }

        if (PhotonNetwork.IsConnected)
        {
            PlayerIndex = PhotonNetwork.CountOfPlayersInRooms + 1;
            VDebug.Instance.Log(PlayerIndex.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
