using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum VisiblePlayer
{
    PLAYER_1 = 1,
    PLAYER_2 = 2,
    BOTH_PLAYER = 3
}

public class InteractableVisible : MonoBehaviour
{
    public VisiblePlayer visiblePlayer = VisiblePlayer.BOTH_PLAYER;
    private bool isOffline = true;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        isOffline = !PhotonNetwork.IsConnected;
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        // which one is current player;
        if (!isOffline)
        {
            foreach (var player in players)
            {
                if (player.GetComponent<PhotonView>().IsMine)
                {
                    // my player
                    this.player = player;
                }
            }
        }
        else
        {
            player = players[0];
        }

        if (isOffline || visiblePlayer == VisiblePlayer.BOTH_PLAYER)
        {
            return;
        }
        // Debug.Log(player);
        if (player.GetComponent<NetworkOwnerShip>().PlayerIndex != (int)visiblePlayer)
        {
            disableNonVisible();
        }
    }

    private void disableNonVisible()
    {
        Debug.Log("Disabled : " + this.gameObject.name);
        Destroy(gameObject.GetComponent<Animator>());
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }
}
