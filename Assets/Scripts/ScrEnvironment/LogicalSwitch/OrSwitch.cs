using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrSwitch : IInteractable
{
    public IInteractable[] switchsToToggle;
    public ITargetable[] Targets;
    public bool isOn = false;

    public override void Interact()
    {
        // do nothing
        return;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        if (switchsToToggle == null)
        {
            Debug.Log("Empty and switch");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isOn = false;
        foreach (Switch sw in switchsToToggle)
        {
            if (sw.isOn)
            {
                isOn = true;
            }
        }

        if (isOn != this.isOn)
        {
            foreach (ITargetable target in Targets)
            {
                target.Turn();
            }
            this.isOn = isOn;
        }
    }
}
