using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    private PhotonView photonView;
    private NetworkRPC networkRPC;
    private bool isOffline = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        networkRPC = GetComponent<NetworkRPC>();

        isOffline = !PhotonNetwork.IsConnected;
    }

    public void NetworkPause()
    {
        if (!isOffline)
        {
            photonView.RPC("PauseGame", RpcTarget.AllBuffered);
        }
        else
        {
            ClientPause();
        }
    }

    public void NetworkUnpause()
    {
        if (!isOffline)
        {
            photonView.RPC("UnpauseGame", RpcTarget.AllBuffered);
        }
        else
        {
            ClientUnpause();
        }
    }

    public void ClientPause()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    public void ClientUnpause()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

}
