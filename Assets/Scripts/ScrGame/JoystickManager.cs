using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{

    #region variables

    private int leftInput = 0;
    private int rightInput = 0;
    private float horizontalRaw = 0;
    
    private bool jumpDown = false;

    public bool interactDown = false;
    public bool pickUpDown = false;

    #endregion

    #region updating

    private void Update()
    {
        horizontalRaw = rightInput - leftInput;
    }

    #endregion

    #region up and down

    public void DownJump()
    {
        if (!jumpDown)
        {
            jumpDown = true;
            StartCoroutine(ReleaseJump());
        }
    }

    public void DownInteract()
    {
        if (!interactDown)
        {
            interactDown = true;
            pickUpDown = true;
            StartCoroutine(ReleaseInteract());
        }
    }

    public void DownLeft()
    {
        leftInput = 1;
    }

    public void UpLeft()
    {
        leftInput = 0;
    }

    public void DownRight()
    {
        rightInput = 1;
    }

    public void UpRight()
    {
        rightInput = 0;
    }

    #endregion

    #region utility

    IEnumerator ReleaseJump()
    {
        yield return null; //wait 1 frame
        //jumpDown = false;
    }

    IEnumerator ReleaseInteract()
    {
        yield return null; //wait 1 frame
        //interactDown = false;
    }

    #endregion

    #region Getter and Setter

    public float GetHorizontalRaw()
    {
        return horizontalRaw;
    }

    public bool GetJumpDown()
    {
        bool temp = jumpDown;
        jumpDown = false;
        return temp;
    }

    public bool GetInteractDown()
    {
        bool temp = interactDown;
        interactDown = false;
        return temp;
    }

    public bool GetPickUpDown()
    {
        bool temp = pickUpDown;
        pickUpDown = false;
        return temp;
    }

    #endregion

}
