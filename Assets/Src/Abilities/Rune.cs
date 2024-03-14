using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rune
{
    public Action OnUse { get; private set; }
    
    public Rune(Action onUse) {
        OnUse = onUse;
    }

    public void Reload() { }
}
