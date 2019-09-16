using UnityEngine;
using Photon.Pun;

public class NetworkRPC : MonoBehaviour
{
    [PunRPC]
    public void Interact(int ID)
    {
        IInteractable interactable = InteractableFactory.Instance.GetInteractable(ID);
        interactable.Interact();
    }
}