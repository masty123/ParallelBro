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
    [HideInInspector]
    public int ID;
    public EffectType effectType = EffectType.EFFECT_BOTH;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public virtual void Start()
    {
        // assign ID;
        // ID = InteractableFactory.Instance.GetID(this);
        // VDebug.Instance.Log(this.gameObject.name + " : " + ID);
    }

    public abstract void Interact();

    public abstract void SelfInteract();
}
