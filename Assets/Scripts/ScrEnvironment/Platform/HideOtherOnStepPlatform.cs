using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOtherOnStepPlatform : MonoBehaviour
{
    public Switch _switch;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if character
        if(collision.gameObject.tag == "player")
        {
            // sent other RPC to toggle switch
            _switch.Interact();
        }
    }
}
