﻿using System;
using UnityEngine;

[Serializable]
public abstract class ITargetable : MonoBehaviour
{
    public abstract void Turn();

    public abstract void Reset();

    public abstract void SelfTurn();
}
