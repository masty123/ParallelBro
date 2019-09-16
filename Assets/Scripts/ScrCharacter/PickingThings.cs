using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickingThings : MonoBehaviour
{
    private PhotonView photonView;
    GameObject toPickUp;
    GameObject holdingItem;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (photonView != null && !photonView.IsMine)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (holdingItem == null)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    PickUpItem();
                }
                else
                {
                    // network stuff send this information to who?
                    RpcTarget target = RpcTarget.AllBuffered;
                    switch (holdingItem.GetComponent<IPickUp>().pickUpType)
                    {
                        case SwitchType.EffectBoth: target = RpcTarget.AllBuffered; break;
                        case SwitchType.EffectOwn: PickUpItem(); return;
                        case SwitchType.EffectOther: target = RpcTarget.OthersBuffered; break;
                    }

                    // sent
                    photonView.RPC("PickUpItem", target);
                }
            }
        }
    }

    [PunRPC]
    private void PickUpItem()
    {
        holdingItem = toPickUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPickUp>())
        {
            toPickUp = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(collision.gameObject, toPickUp))
        {
            toPickUp = null;
        }
    }

}
