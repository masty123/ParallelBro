using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : ITargetable
{
    public bool isToggle = false;
    private bool isOn = false;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Turn()
    {
        if (isToggle)
        {
            isOn = !isOn;
        }
        else
        {
            if (isOn)
            {
                return;
            }
            isOn = true;
        }

        // handle animation
        _Turn();
    }

    private void _Turn()
    {
        animator.SetBool("IsOn", isOn);
    }

    public override void Reset()
    {
        isToggle = false;
        isOn = false;
        _Turn();
    }
}
