using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPickUp : MonoBehaviour
{

    public SwitchType pickUpType = SwitchType.EffectOwn;
    public abstract bool use();

}
