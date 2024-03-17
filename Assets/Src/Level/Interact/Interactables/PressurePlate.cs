using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : PressInteractable
{
    private void Awake()
    {
        Subscribe("isActive", stateChanged);
    }

    private void stateChanged()
    {
        Debug.Log("IsAcitve");
    }
}
