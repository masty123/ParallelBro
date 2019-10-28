﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndSwitch : MonoBehaviour
{
    public Switch[] switchsToToggle;
    public ITargetable[] Targets;
    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        if (switchsToToggle == null)
        {
            Debug.Log("Empty and switch");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isOn = true;
        foreach (Switch sw in switchsToToggle)
        {
            if (!sw.isOn)
            {
                isOn = false;
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
