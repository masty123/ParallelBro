using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkOwnerShip : MonoBehaviourPun
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            spriteRenderer.color = Color.black;
            Debug.Log("Color Changed");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
