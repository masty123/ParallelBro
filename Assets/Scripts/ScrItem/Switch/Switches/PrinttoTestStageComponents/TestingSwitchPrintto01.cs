using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSwitchPrintto01 : ITurningSwitch
{

    public GameObject BoxToChangeColor;

    Animator anim;
    Animator boxAnim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        if (IsOn)
        {
            anim.SetTrigger("TurnOn");
        }
        else
        {
            anim.SetTrigger("TurnOff");
        }
        boxAnim = BoxToChangeColor.GetComponent<Animator>();
    }

    public override void Interact()
    {
        VDebug.Instance.Log("Interacted");
        IsOn = true;
        if (IsOn)
        {
            if (anim)
            {
                anim.SetTrigger("TurnOn");
            }
            boxAnim.SetTrigger("Active");
        }
        else
        {
            //anim.SetTrigger("TurnOff");
        }
    }
}
