using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviour
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

    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // instancate player prefab
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
    }
}
