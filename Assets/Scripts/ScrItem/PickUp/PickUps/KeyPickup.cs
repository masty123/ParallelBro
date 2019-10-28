using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * This script is used for testing pick up that can be destroyed at the specific game object
 * 
 */
public class KeyPickup : IPickUp
{
    
    public IInteractable keyHoleToPutIn;
    private NetworkRPC networkRPC;

    public override void Start()
    {
        base.Start();
        networkRPC = GetComponent<NetworkRPC>();
    }

    public override void Interact()
    {
        Debug.Log("Try in insert key");
        if (isUsable)
        {
            // Force activate switch
            networkRPC.Interact(keyHoleToPutIn.ID);
            Destroy(gameObject);
        }
        else
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameObject.ReferenceEquals(keyHoleToPutIn, other.gameObject))
        {
            Debug.Log("The fruit hit the gate");
            SetUsable(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(keyHoleToPutIn, collision.gameObject))
        {
            SetUsable(false);
        }
    }

}
