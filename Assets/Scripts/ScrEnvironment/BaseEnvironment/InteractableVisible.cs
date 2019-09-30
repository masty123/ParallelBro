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
    public bool hideRenderer = true;
    public bool hideRigidbody = true;
    public bool hideAnimator = true;
    public bool hideCollider = true;
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
                Debug.Log(player.name);
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
            VDebug.Instance.Log(player.GetComponent<NetworkOwnerShip>().PlayerIndex.ToString());
            disableNonVisible();
        }
    }

    private void disableNonVisible()
    {
        Debug.Log("Disabled : " + this.gameObject.name);
        if (hideRenderer)
        {
            this.GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (hideCollider)
        {
            this.GetComponent<Collider2D>().enabled = false;
        }

        if (hideAnimator)
        {
            this.GetComponent<Animator>().enabled = false;
        }

        if (hideRigidbody)
        {
            this.GetComponent<Rigidbody2D>().simulated = false;
        }
        // Destroy(gameObject.GetComponent<Animator>());
        // this.GetComponent<SpriteRenderer>().color = Color.black;
        // Destroy(gameObject.GetComponent<Collider2D>());
        // Destroy(gameObject.GetComponent<Rigidbody2D>());
    }
}
