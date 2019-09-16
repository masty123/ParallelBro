using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPickUp : MonoBehaviour
{

    public bool isUsable = false;

    public abstract bool use();

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

}
