using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<CoupleInteractables> coupleInteractables; 

    private void Start()
    {
        foreach (var couple in coupleInteractables)
        {
            couple.Broadcaster.Subscribe("isActive", (state, prev) => couple.Receiver.ChangeObjectState("isActive", true));
        }
    }
}

[Serializable]
public struct CoupleInteractables
{
    public StateObject Broadcaster;
    public StateObject Receiver;
}