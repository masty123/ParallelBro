using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickingThings : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject toPickUp;
    public GameObject holdingItem;

    JoystickManager controllerListener;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        controllerListener = GameObject.Find("ControllerListener").GetComponent<JoystickManager>();
    }

    private void Update()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        #region Keyboard inputs
        if (Input.GetButtonDown("Interact"))
        {
            pickItUp();
        }
        #endregion

        #region On-screen inputs
        if(controllerListener != null)
        {
            if (controllerListener.GetPickUpDown())
            {
                pickItUp();
            }
        }
        #endregion

    }

    private void pickItUp()
    {
        Debug.Log(holdingItem);
        if (holdingItem == null)
        {
            if (toPickUp != null)
            {
                if(toPickUp.transform.parent == null)
                {
                    // get to pick up id
                    int id = toPickUp.GetComponent<IPickUp>().ID;
                    // fire action to player
                    GetComponent<PlayerAction>().PickUp(id);
                    // PickUpItem();
                }
            }
        }
        else if (holdingItem.GetComponent<IPickUp>().isUsable)
        {
            //UseItem();
            int id = holdingItem.GetComponent<IPickUp>().ID;
            GetComponent<PlayerAction>().UseItem(id);
        }
        else
        {
            //DropItem();
            int id = holdingItem.GetComponent<IPickUp>().ID;
            GetComponent<PlayerAction>().DropDown(id);
        }
    }

    /*
    private void UseItem()
    {
        holdingItem.transform.SetParent(null);
        holdingItem.GetComponent<Rigidbody2D>().isKinematic = false;
        holdingItem.GetComponent<IPickUp>().Interact();
    }
    
    private void PickUpItem()
    {
        holdingItem = toPickUp;
        holdingItem.GetComponent<Rigidbody2D>().isKinematic = true;
        holdingItem.transform.SetParent(this.gameObject.transform);
        holdingItem.transform.localPosition = new Vector3(0, -0.07f, -0.1f);
        holdingItem.GetComponent<IPickUp>().OnPickUp();
    }
    
    private void DropItem()
    {
        holdingItem.transform.SetParent(null);
        holdingItem.GetComponent<Rigidbody2D>().isKinematic = false;
        holdingItem.GetComponent<IPickUp>().OnDrop();
        holdingItem = null;
    }
    */

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
