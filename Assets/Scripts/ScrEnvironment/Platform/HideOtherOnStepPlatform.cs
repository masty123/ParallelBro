using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HideOtherOnStepPlatform : MonoBehaviour
{
    public Switch _switch;
    public bool isOn = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOn)
        {
            return;
        }
        // if character
        if (collision.gameObject.tag == "player")
        {
            if (PhotonNetwork.IsConnected)
            {
                // sent other RPC to toggle switch
                collision.gameObject.GetComponent<PlayerAction>().Interact(_switch.ID);
                // _switch.Interact();
            }
            isOn = !isOn;
        }
    }
}
