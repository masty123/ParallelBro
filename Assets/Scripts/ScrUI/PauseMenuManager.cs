using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseCanvas;

    public void NetworkPause()
    {
        if (PhotonNetwork.IsConnected)
        {
            FindObjectOfType<PhotonView>().RPC("PauseGame", RpcTarget.AllBuffered);
            //photonView.RPC("PauseGame", RpcTarget.AllBuffered);
        }
        else
        {
            ClientPause();
        }
    }

    public void NetworkUnpause()
    {
        if (PhotonNetwork.IsConnected)
        {
            FindObjectOfType<PhotonView>().RPC("UnpauseGame", RpcTarget.AllBuffered);
            //photonView.RPC("UnpauseGame", RpcTarget.AllBuffered);
        }
        else
        {
            Debug.Log("Offline unpaused");
            ClientUnpause();
        }
    }

    public void ClientPause()
    {
        Debug.Log("Paused");
        Time.timeScale = float.MinValue;
        pauseCanvas.SetActive(true);
    }

    public void ClientUnpause()
    {
        Debug.Log("Unpaused");
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

}
