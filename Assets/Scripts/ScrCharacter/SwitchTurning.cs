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
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (turningSwitch.Count != 0)
            {
                if (PhotonNetwork.OfflineMode)
                {
                    TurnSwitch();
                }
                else
                {
                    // network stuff send this information to who?
                    RpcTarget target = RpcTarget.AllBuffered;
                    switch (turningSwitch.Peek().switchType)
                    {
                        case SwitchType.EffectBoth: target = RpcTarget.AllBuffered; break;
                        case SwitchType.EffectOwn: TurnSwitch(); return;
                        case SwitchType.EffectOther: target = RpcTarget.OthersBuffered; break;
                    }

                    // sent
                    photonView.RPC("TurnSwitch", target);
                }
            }
        }
    }

    [PunRPC]
    private void TurnSwitch()
    {
        turningSwitch.Peek().turn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>())
        {
            turningSwitch.Push(collision.gameObject.GetComponent<ITurningSwitch>());
            Debug.Log(collision + " Added");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>())
        {
            turningSwitch.Pop();
            Debug.Log(collision + " Removed");
        }
    }

}
