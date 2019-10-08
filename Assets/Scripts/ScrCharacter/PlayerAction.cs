using UnityEngine;
using Photon.Pun;

public class PlayerAction : MonoBehaviour
{
    private PhotonView photonView;
    private NetworkRPC networkRPC;
    private bool isOffline = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        networkRPC = GetComponent<NetworkRPC>();

        isOffline = !PhotonNetwork.IsConnected;
    }

    public void Ready()
    {
        networkRPC.Ready();
    }

    public void Interact(int ID)
    {
        // do network thing here;
        IInteractable interactable = InteractableFactory.Instance.GetInteractable(ID);
        if (isOffline)
        {
            // logic at here
            networkRPC.Interact(interactable.ID);
            return;
        }

        switch (interactable.effectType)
        {
            case EffectType.EFFECT_BOTH: photonView.RPC("Interact", RpcTarget.AllBuffered, ID); break;
            case EffectType.EFFECT_OWN: networkRPC.Interact(interactable.ID); break;
            case EffectType.EFFECT_OTHER: photonView.RPC("Interact", RpcTarget.OthersBuffered, ID); break;
        }
    }

    public void PickUp(int ID)
    {
        IInteractable interactable = InteractableFactory.Instance.GetInteractable(ID);
        if (isOffline)
        {
            // logic at here
            networkRPC.PickUp(interactable.ID);
            return;
        }

        switch (interactable.effectType)
        {
            case EffectType.EFFECT_BOTH: photonView.RPC("PickUp", RpcTarget.AllBuffered, ID); break;
            case EffectType.EFFECT_OWN: networkRPC.Interact(interactable.ID); break;
            case EffectType.EFFECT_OTHER: photonView.RPC("PickUp", RpcTarget.OthersBuffered, ID); break;
        }
    }

    public void DropDown(int ID)
    {
        IInteractable interactable = InteractableFactory.Instance.GetInteractable(ID);
        if (isOffline)
        {
            // logic at here
            networkRPC.DropDown(interactable.ID);
            return;
        }

        switch (interactable.effectType)
        {
            case EffectType.EFFECT_BOTH: photonView.RPC("DropDown", RpcTarget.AllBuffered, ID); break;
            case EffectType.EFFECT_OWN: networkRPC.Interact(interactable.ID); break;
            case EffectType.EFFECT_OTHER: photonView.RPC("DropDown", RpcTarget.OthersBuffered, ID); break;
        }
    }

    public void UseItem(int ID)
    {
        IInteractable interactable = InteractableFactory.Instance.GetInteractable(ID);
        if (isOffline)
        {
            // logic at here
            networkRPC.UseItem(interactable.ID);
            return;
        }

        switch (interactable.effectType)
        {
            case EffectType.EFFECT_BOTH: photonView.RPC("UseItem", RpcTarget.AllBuffered, ID); break;
            case EffectType.EFFECT_OWN: networkRPC.Interact(interactable.ID); break;
            case EffectType.EFFECT_OTHER: photonView.RPC("UseItem", RpcTarget.OthersBuffered, ID); break;
        }
    }

    public void ChangeOwnership(int ID)
    {
        IInteractable interactable = InteractableFactory.Instance.GetInteractable(ID);
        if(isOffline)
        {
            networkRPC.ChangeOwnership(interactable.ID);
            return;
        }

        switch (interactable.effectType)
        {
            case EffectType.EFFECT_BOTH: photonView.RPC("ChangeOwnership", RpcTarget.AllBuffered, ID); break;
            case EffectType.EFFECT_OWN: networkRPC.Interact(interactable.ID); break;
            case EffectType.EFFECT_OTHER: photonView.RPC("ChangeOwnership", RpcTarget.OthersBuffered, ID); break;
        }
    }
}