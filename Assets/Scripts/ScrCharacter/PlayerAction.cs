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

}