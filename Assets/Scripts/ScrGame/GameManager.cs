using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public Text RoomCodeText;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        RoomCodeText.text = PhotonNetwork.CurrentRoom.Name;
        foreach(var player in PhotonNetwork.PlayerList)
        {
            Debug.Log(player.NickName);
        }
    }
}
