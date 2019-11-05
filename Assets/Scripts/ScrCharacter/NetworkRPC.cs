using UnityEngine;
using Photon.Pun;

public class NetworkRPC : MonoBehaviour
{
    [PunRPC]
    public void Ready()
    {
        VDebug.Instance.Log("Ready");
    }

    [PunRPC]
    public void SetAll(int playerIndex)
    {
        GetComponent<NetworkOwnerShip>().SetPlayerIndex(playerIndex);
    }

    [PunRPC]
    public void RequestPlayerIndex()
    {
        // GameManager
        GameObject gm = GameObject.FindGameObjectWithTag("gamemanager");
        gm.GetComponent<GameManager>().playerIndex();
    }

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
        GetComponent<PickingThings>().holdingItem.GetComponent<IPickUp>().Interact();
    }

    [PunRPC]
    public void ChangeOwnership(int ID)
    {
        IInteractable interactable = InteractableFactory.Instance.GetInteractable(ID);
        InteractableVisible visible = interactable.GetComponent<InteractableVisible>();
        if (visible.visiblePlayer == VisiblePlayer.PLAYER_1)
        {
            visible.visiblePlayer = VisiblePlayer.PLAYER_2;
        }
        else if (visible.visiblePlayer == VisiblePlayer.PLAYER_2)
        {
            visible.visiblePlayer = VisiblePlayer.PLAYER_1;
        }
        visible.recheckVisible();
        GetComponent<PickingThings>().toPickUp = null;
        GameObject endChecker = GameObject.Find("EndChecker");
        if (endChecker != null)
        {
            EndChecker endCheckerScript = endChecker.GetComponent<EndChecker>();
            if (endCheckerScript.isEnd())
            {
                endCheckerScript.endPopup.SetActive(true);
            }
        }
    }

    [PunRPC]
    public void PauseGame()
    {

    }

    [PunRPC]
    public void UnpauseGame()
    {

    }

    [PunRPC]
    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
    }
}