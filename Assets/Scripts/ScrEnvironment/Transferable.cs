using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Transferable : MonoBehaviour
{
    private VisiblePlayer visiblePlayer;
    private InteractableVisible visibility;

    private void Start()
    {
        visibility = this.GetComponent<InteractableVisible>();
        visiblePlayer = visibility.visiblePlayer;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "transfer" && this.gameObject.transform.parent == null && visiblePlayer == visibility.visiblePlayer)
        {
            Debug.Log("Dropped at gate");
            // Change Ownership
            ChangeOwnership(this.GetComponent<IInteractable>().ID);
        }
    }

    private void ChangeOwnership(int ID)
    {
        bool isOffline = !PhotonNetwork.IsConnected;
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        // which one is current player;
        if (!isOffline)
        {
            foreach (var player in players)
            {
                if (player.GetComponent<PhotonView>().IsMine)
                {
                    // my player
                    player.GetComponent<PlayerAction>().ChangeOwnership(ID);
                }
            }
        }
        else
        {
            players[0].GetComponent<PlayerAction>().ChangeOwnership(ID);
        }
    }
}
