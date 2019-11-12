using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *
 * This script is used for testing pick up that can be destroyed at the specific game object
 * 
 */
public class TestingPickUpDestroyAtObject : IPickUp
{

    public GameObject target;

    public override void Interact()
    {
        Debug.Log("Eating the fruit at the gate");
        if (isUsable)
        {
            // use item at gate

            // Destroy(gameObject);
        }
        else
        {

        }
    }

    public override void SelfInteract()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameObject.ReferenceEquals(target, other.gameObject))
        {
            Debug.Log("The fruit hit the gate");
            SetUsable(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameObject.ReferenceEquals(target, collision.gameObject))
        {
            SetUsable(false);
        }
    }

}
