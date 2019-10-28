using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XorSwitch : Switch
{
    public Switch[] switchsToToggle = new Switch[2];

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
        bool isOn = switchsToToggle[0].isOn != switchsToToggle[1].isOn;

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
