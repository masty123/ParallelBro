using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchTurning : MonoBehaviour
{
    private PhotonView photonView;
    Switch turningSwitch = null;

    JoystickManager controllerListener;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        controllerListener = GameObject.Find("ControllerListener").GetComponent<JoystickManager>();
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
            if (turningSwitch != null)
            {
                int id = turningSwitch.ID;
                GetComponent<PlayerAction>().Interact(id);
            }
        }

        if (controllerListener != null)
        {
            if (controllerListener.GetInteractDown())
            {
                if (turningSwitch != null)
                {
                    int id = turningSwitch.ID;
                    GetComponent<PlayerAction>().Interact(id);
                }
            }
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
