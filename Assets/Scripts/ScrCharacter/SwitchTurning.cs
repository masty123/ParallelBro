using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTurning : MonoBehaviour
{

    Stack<ITurningSwitch> turningSwitch;

    private void Start()
    {
        turningSwitch = new Stack<ITurningSwitch>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (turningSwitch.Count != 0)
            {
                turningSwitch.Peek().turn();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>())
        {
            turningSwitch.Push(collision.gameObject.GetComponent<ITurningSwitch>());
            Debug.Log(collision + " Added");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ITurningSwitch>())
        {
            turningSwitch.Pop();
            Debug.Log(collision + " Removed");
        }
    }

}
