using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public Character2DController controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;

    private void Start()
    {
        // PhotonNetwork.OfflineMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView != null && !photonView.IsMine)
        {
            return;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

    }

    private void FixedUpdate()
    {
         if (photonView != null && !photonView.IsMine)
        {
            return;
        }
        //Move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
