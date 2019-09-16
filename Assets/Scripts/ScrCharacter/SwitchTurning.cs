using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchTurning : MonoBehaviour
{
    private PhotonView photonView;
    Stack<ITurningSwitch> turningSwitch;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        turningSwitch = new Stack<ITurningSwitch>();
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
            if (turningSwitch.Count != 0)
            {
                int id = turningSwitch.Peek().ID;
                GetComponent<PlayerAction>().Interact(id);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>())
        {
            turningSwitch.Push(collision.gameObject.GetComponent<ITurningSwitch>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>())
        {
            turningSwitch.Pop();
        }
    }

}
