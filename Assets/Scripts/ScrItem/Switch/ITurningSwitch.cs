using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ITurningSwitch : IInteractable
{
    private bool isOn = false;

    public bool IsOn
    {
        get
        {
            return isOn;
        }

        set
        {
            isOn = value;
        }
    }

    public override void Interact()
    {
        Debug.LogWarning("System is not implemented");
    }
}
