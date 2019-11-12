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
        if(turningSwitch != null)
        {
            if (turningSwitch.requiredKey != null && pickingThings.holdingItem != null)
            {
                if(GameObject.ReferenceEquals(turningSwitch.requiredKey, pickingThings.holdingItem))
                {
                    int id = turningSwitch.ID;
                    GetComponent<PlayerAction>().Interact(id);
                }
            }
            else if (turningSwitch.requiredKey == null && pickingThings.holdingItem == null && pickingThings.toPickUp == null)
            {
                int id = turningSwitch.ID;
                GetComponent<PlayerAction>().Interact(id);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Switch collisionSwitch = collision.gameObject.GetComponent<Switch>();

        if (collisionSwitch != null)
        {
            if (!collisionSwitch.isForStepOn)
            {
                turningSwitch = collision.gameObject.GetComponent<Switch>();
            }
            // This is for step in switches
            else
            {
                // Toggle right away to step in
                int id = collisionSwitch.ID;
                GetComponent<PlayerAction>().Interact(id);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        Switch collisionSwitch = collision.gameObject.GetComponent<Switch>();

        if (collisionSwitch != null)
        {
            if (collisionSwitch.Equals(turningSwitch))
            {
                turningSwitch = null;
            }
            // Disable step on switch when leave
            if (collisionSwitch.isForStepOn)
            {
                int id = collisionSwitch.ID;
                // Toggle again to turn off switch
                GetComponent<PlayerAction>().Interact(id);
            }
        }

    }

}
