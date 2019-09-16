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
        IsOn = true;
        if (IsOn)
        {
            anim.SetTrigger("TurnOn");
            boxAnim.SetTrigger("Active");
        }
        else
        {
            //anim.SetTrigger("TurnOff");
        }
    }
}
