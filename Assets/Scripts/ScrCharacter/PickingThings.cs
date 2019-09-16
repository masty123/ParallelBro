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
                if(toPickUp != null)
                {
                    PickUpItem();
                }
            }
            else if (holdingItem.GetComponent<IPickUp>().isUsable)
            {
                UseItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    [PunRPC]
    private void UseItem()
    {
        holdingItem.GetComponent<IPickUp>().use();
    }

    [PunRPC]
    private void PickUpItem()
    {
        holdingItem = toPickUp;
        holdingItem.GetComponent<Rigidbody2D>().isKinematic = true;
        holdingItem.transform.SetParent(this.gameObject.transform);
        holdingItem.transform.localPosition = new Vector3(0, -0.07f, -0.1f);
    }

    [PunRPC]
    private void DropItem()
    {
        holdingItem.transform.SetParent(null);
        holdingItem.GetComponent<Rigidbody2D>().isKinematic = false;
        holdingItem = null;
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
