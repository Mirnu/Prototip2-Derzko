using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public dynamic state = new
    {
        isEnabled = false,
        time = 0,
    };

    public Action<dynamic> StateChanged;

    private void Awake()
    {
        StateChanged += Changed;
    } 

    private void Changed(dynamic state)
    {
        
    }
}
