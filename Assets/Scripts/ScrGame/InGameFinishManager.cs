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
            PhotonNetwork.Disconnect();
        }
    }

    public void LoadLevel(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
