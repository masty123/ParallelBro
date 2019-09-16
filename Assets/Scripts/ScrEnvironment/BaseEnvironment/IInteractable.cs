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
    [PunRPC]
    public void Interact(int ID)
    {
        Debug.Log("Interact ID : " + ID);
        InteractableFactory.Instance.GetInteractable(ID).Interact();
    }

    public void NotifyNetwork(PhotonView photonView)
    {
        if (photonView == null)
        {
            Debug.LogWarning("You have called NotifyNetwork in Offline instance");
            Interact();
            return;
        }

        switch (effectType)
        {
            case EffectType.EFFECT_BOTH: photonView.RPC("Interact", RpcTarget.AllBuffered, ID); break;
            case EffectType.EFFECT_OWN: Interact(ID); break;
            case EffectType.EFFECT_OTHER: photonView.RPC("Interact", RpcTarget.OthersBuffered, ID); break;
        }
    }
}
