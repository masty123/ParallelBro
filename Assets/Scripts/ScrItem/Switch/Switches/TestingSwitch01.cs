using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSwitch01 : ITurningSwitch
{

    Animator anim;

    public void Start()
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
    }

    public override void Interact()
    {
        IsOn = !IsOn;
        if (IsOn)
        {
            anim.SetTrigger("TurnOn");
            Debug.Log("Switch On");
        }
        else
        {
            anim.SetTrigger("TurnOff");
            Debug.Log("Switch Off");
        }
    }

}
