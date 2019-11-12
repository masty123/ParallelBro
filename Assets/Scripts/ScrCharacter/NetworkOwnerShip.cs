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
        else
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                SetPlayerIndex(UserData.GetInstance().GetCharacterIndex());
            }
            else
            {
                SetPlayerIndex(UserData.GetInstance().GetCharacterIndex() == 1 ? 2 : 1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            Character1.GetComponent<SpriteRenderer>().color = Color.black;
            Character2.GetComponent<SpriteRenderer>().color = Color.black;
            gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
            Character1.GetComponent<SpriteRenderer>().sortingLayerName = "Shadow";
            Character2.GetComponent<SpriteRenderer>().sortingLayerName = "Shadow";
        }
        else
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().Player = this.gameObject;
            Character1.GetComponent<SpriteRenderer>().color = Color.white;
            Character2.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.layer = LayerMask.NameToLayer("Player");
            Character1.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            Character2.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        }
    }

    public void SetPlayerIndex(int playerIndex)
    {
        this.PlayerIndex = playerIndex;
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient && GetComponent<PhotonView>().IsMine)
        {
            UserData.GetInstance().SetCharacterIndex(playerIndex);
        }
        checkCharacterSprite();
        // checkVisibility();
    }

    private void checkVisibility()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("interactable");
        foreach (GameObject go in gos)
        {
            InteractableVisible iv = go.GetComponent<InteractableVisible>();
            if (iv == null)
            {
                continue;
            }
            iv.recheckVisible();
        }
    }


    private void checkCharacterSprite()
    {
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
        Start();
    }
}
