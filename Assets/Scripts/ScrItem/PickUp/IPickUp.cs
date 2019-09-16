using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPickUp : IInteractable
{

    public bool isUsable = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        foreach (Collider2D colliders in player.GetComponents<Collider2D>())
        {
            Physics2D.IgnoreCollision(colliders, GetComponent<CircleCollider2D>());
        }
    }

    public void SetUsable(bool status)
    {
        isUsable = status;
    }

    //This function will be called when player pickup the item
    public void OnPickUp() { }

    //This function will be called when player drop the item
    public void OnDrop() { }

}
