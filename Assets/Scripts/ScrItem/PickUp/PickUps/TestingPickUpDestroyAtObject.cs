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

    public override bool use()
    {
        if (isUsable)
        {
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameObject.ReferenceEquals(target, other.gameObject))
        {
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
