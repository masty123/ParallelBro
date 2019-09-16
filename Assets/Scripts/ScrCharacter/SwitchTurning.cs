using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchTurning : MonoBehaviour
{
    private PhotonView photonView;
    ITurningSwitch turningSwitch = null;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        // skip other player action
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (turningSwitch != null)
            {
                int id = turningSwitch.ID;
                GetComponent<PlayerAction>().Interact(id);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>())
        {
            turningSwitch = collision.gameObject.GetComponent<ITurningSwitch>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>().Equals(turningSwitch))
        {
            turningSwitch = null;
        }
    }

}
