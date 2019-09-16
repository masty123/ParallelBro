using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchType
{
    EffectOwn,
    EffectBoth,
    EffectOther
}

public abstract class ITurningSwitch : MonoBehaviour
{
    public SwitchType switchType = SwitchType.EffectBoth;
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
