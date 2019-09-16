﻿using System.Collections;
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
    public int ID;
    public EffectType effectType = EffectType.EFFECT_BOTH;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // assign ID;
        ID = InteractableFactory.Instance.GetID(this);
    }

    public abstract void Interact();
}
