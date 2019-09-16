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

    [PunRPC]
    public void PickUp(int ID)
    {
        //GameObject holdingItem = GetComponent<PickingThings>().holdingItem;
        GetComponent<PickingThings>().holdingItem = InteractableFactory.Instance.GetInteractable(ID).gameObject;
        GetComponent<PickingThings>().holdingItem.GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<PickingThings>().holdingItem.transform.SetParent(this.gameObject.transform);
        GetComponent<PickingThings>().holdingItem.transform.localPosition = new Vector3(0, -0.07f, -0.1f);
        GetComponent<PickingThings>().holdingItem.GetComponent<IPickUp>().OnPickUp();
    }

    [PunRPC]
    public void DropDown(int ID)
    {
        GetComponent<PickingThings>().holdingItem = InteractableFactory.Instance.GetInteractable(ID).gameObject;
        GetComponent<PickingThings>().holdingItem.transform.SetParent(null);
        GetComponent<PickingThings>().holdingItem.GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<PickingThings>().holdingItem.GetComponent<IPickUp>().OnDrop();
        GetComponent<PickingThings>().holdingItem = null;
    }

    [PunRPC]
    public void UseItem(int ID)
    {
        GetComponent<PickingThings>().holdingItem = InteractableFactory.Instance.GetInteractable(ID).gameObject;
        GetComponent<PickingThings>().holdingItem.transform.SetParent(null);
        GetComponent<PickingThings>().holdingItem.GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<PickingThings>().holdingItem.GetComponent<IPickUp>().Interact();
    }

}