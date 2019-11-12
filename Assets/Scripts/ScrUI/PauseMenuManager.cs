using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject restartButton;

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
        pauseCanvas.SetActive(true);
        //StartCoroutine(ChangeTimeScale(0.5f));
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient)
        {
            restartButton.gameObject.SetActive(false);
        }
    }

    IEnumerator ChangeTimeScale(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 0.01f;
    }

    public void ClientUnpause()
    {
        Debug.Log("Unpaused");
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public void OnDisconnectPressed()
    {
        Debug.Log("Disconnect pressed");
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

    public void OnRestartPressed()
    {
        PhotonNetwork.LoadLevel("Loading");
    }

}
