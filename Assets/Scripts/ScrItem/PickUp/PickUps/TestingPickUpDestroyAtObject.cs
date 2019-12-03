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
    public Sprite BKey;

    public Sprite OKey;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public override void Start()
    {
        base.Start();
        VisiblePlayer vp = this.GetComponent<InteractableVisible>().visiblePlayer;
        if (vp == VisiblePlayer.PLAYER_1)
        {
            this.GetComponent<SpriteRenderer>().sprite = BKey;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = OKey;
        }
    }

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
