using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameFinishManager : MonoBehaviour
{
    public GameObject[] nonHosts;
    public GameObject[] Hosts;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            foreach (GameObject nonHost in nonHosts)
            {
                nonHost.SetActive(false);
            }
            return;
        }
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            foreach (GameObject nonHost in nonHosts)
            {
                nonHost.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject Host in Hosts)
            {
                Host.SetActive(false);
            }
        }
    }

    public void LoadMainMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("player");
            foreach (GameObject go in gos)
            {
                if (go.GetComponent<PhotonView>().IsMine)
                {
                    PlayerAction pa = go.GetComponent<PlayerAction>();
                    pa.Disconnect();
                }
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManager>().OnLeftRoom();
        }
    }


    public void LoadLevel(string sceneName)
    {
        Debug.Log(sceneName);
        Debug.Log(sceneName.Contains("level"));
        // set room data
        int level;
        if (sceneName.Contains("level"))
        {
            level = int.Parse(sceneName.Substring(5, 1));
        }
        else
        {
            level = int.Parse(sceneName);
        }

        RoomData.GetInstance().level = level;
        // sent rpc change other room data

        PhotonNetwork.LoadLevel(sceneName);
    }


}
