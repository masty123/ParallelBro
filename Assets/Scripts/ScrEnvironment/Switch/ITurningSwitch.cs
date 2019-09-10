using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ITurningSwitch : MonoBehaviour
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

    public abstract bool turn();

}
