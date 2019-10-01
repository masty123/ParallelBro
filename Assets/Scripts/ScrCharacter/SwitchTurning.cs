using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchTurning : MonoBehaviour
{
    private PhotonView photonView;
    Switch turningSwitch = null;

    JoystickManager controllerListener;
    PickingThings pickingThings;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        controllerListener = GameObject.Find("ControllerListener").GetComponent<JoystickManager>();
        pickingThings = GetComponent<PickingThings>();
    }

    private void Update()
    {
        // skip other player action
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        if (Input.GetButtonDown("Interact"))
        {
            turnItOn();
        }

        if (controllerListener != null)
        {
            if (controllerListener.GetInteractDown())
            {
                turnItOn();
            }
        }
    }

    private void turnItOn()
    {
        if (turningSwitch != null && pickingThings.holdingItem == null && pickingThings.toPickUp == null)
        {
            int id = turningSwitch.ID;
            GetComponent<PlayerAction>().Interact(id);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Switch>())
        {
            turningSwitch = collision.gameObject.GetComponent<Switch>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Switch>())
        {
            if (collision.gameObject.GetComponent<Switch>().Equals(turningSwitch))
            {
                turningSwitch = null;
            }
        }

    }

}
