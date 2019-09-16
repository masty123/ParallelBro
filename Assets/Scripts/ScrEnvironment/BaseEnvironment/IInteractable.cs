using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum EffectType
{
    EFFECT_BOTH,
    EFFECT_OWN,
    EFFECT_OTHER
}

public abstract class IInteractable : MonoBehaviourPun
{
    private int ID;
    public EffectType effectType = EffectType.EFFECT_BOTH;

    // Start is called before the first frame update
    void Start()
    {
        // assign ID;
        ID = InteractableFactory.Instance.GetID();
    }

    public abstract void Interact();
}
