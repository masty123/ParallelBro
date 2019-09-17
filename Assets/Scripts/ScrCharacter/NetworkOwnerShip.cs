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
        // is other player
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            spriteRenderer.color = Color.black;
            Debug.Log("Color Changed");
            gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
            Transform[] children = gameObject.GetComponentsInChildren<Transform>();
            foreach(Transform child in children)
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
