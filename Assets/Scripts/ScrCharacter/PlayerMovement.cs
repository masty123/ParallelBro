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

    private JoystickManager controllerListener;

    private void Start()
    {
        // PhotonNetwork.OfflineMode = true;
        controllerListener = GameObject.Find("ControllerListener").GetComponent<JoystickManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        #region Keyboard inputs
        //keyboard input
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        #endregion

        #region On-screen inputs
        //Onscreen input
        if (controllerListener != null)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                horizontalMove = controllerListener.GetHorizontalRaw() * runSpeed;
            }
            /*
            if ( controllerListener.GetJumpDown())
            {
                jump = true;
            }
            */
        }
        #endregion

    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        //Onscreen input
        if (controllerListener != null)
        {
            if (controllerListener.GetJumpDown())
            {
                jump = true;
            }
        }

        //Move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
