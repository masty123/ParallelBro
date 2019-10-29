using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

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

    private List<SpriteRenderer> spl = new List<SpriteRenderer>();
    private List<Rigidbody2D> rbl = new List<Rigidbody2D>();
    private List<Animator> aml = new List<Animator>();
    private List<Collider2D> cdl = new List<Collider2D>();
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

        if (hideAnimator)
        {
            aml = getList<Animator>();
        }
        if (hideCollider)
        {
            cdl = getList<Collider2D>();
        }
        if (hideRenderer)
        {
            spl = getList<SpriteRenderer>();
        }
        if (hideRigidbody)
        {
            rbl = getList<Rigidbody2D>();
        }

        if (player.GetComponent<NetworkOwnerShip>().PlayerIndex != (int)visiblePlayer)
        {
            disableNonVisible();
        }
    }

    private List<T> getList<T>()
    {
        List<T> list = new List<T>();
        T[] go = GetComponents<T>();
        list.AddRange(go);
        T[] goc = GetComponentsInChildren<T>();
        list.AddRange(goc);
        return list;
    }

    private void disableNonVisible()
    {
        if (hideRenderer)
        {
            foreach (SpriteRenderer sp in spl)
            {
                sp.color = Color.black;
                sp.sortingLayerName = "Shadow";
            }
        }

        if (hideCollider)
        {
            foreach (Collider2D cd in cdl)
            {
                cd.enabled = false;
            }
        }

        if (hideAnimator)
        {
            foreach (Animator am in aml)
            {
                am.enabled = false;
            }
        }

        if (hideRigidbody)
        {
            foreach (Rigidbody2D rb in rbl)
            {
                rb.simulated = false;
            }
        }
    }

    public void recheckVisible()
    {
        if (hideRenderer)
        {
            foreach (SpriteRenderer sp in spl)
            {
                sp.color = Color.white;
                sp.sortingLayerName = "Interactable";
            }
        }

        if (hideCollider)
        {
            foreach (Collider2D cd in cdl)
            {
                cd.enabled = true;
            }
        }

        if (hideAnimator)
        {
            foreach (Animator am in aml)
            {
                am.enabled = true;
            }
        }

        if (hideRigidbody)
        {
            foreach (Rigidbody2D rb in rbl)
            {
                rb.simulated = true;
            }
        }

        Start();
    }
}
