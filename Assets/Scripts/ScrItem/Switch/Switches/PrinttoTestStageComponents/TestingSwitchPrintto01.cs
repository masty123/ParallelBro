using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSwitchPrintto01 : ITurningSwitch
{

    public GameObject BoxToChangeColor;

    Animator anim;
    Animator boxAnim;

    private void Start()
    {
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

    public override bool turn()
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
        return IsOn;
    }
}
