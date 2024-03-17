using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<StateObject> _stateObjects;

    private void Start()
    {
        foreach (var stateObject in _stateObjects)
        {
            stateObject.ChangeObjectState("isActive", true);
        }
    }
}
