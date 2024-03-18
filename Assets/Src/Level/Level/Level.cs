using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : LevelState
{
    [SerializeField] protected List<CoupleInteractables> coupleInteractables; 

    private void Start()
    {
        foreach (var couple in coupleInteractables)
        {
            _stateObjects.Insert(couple.Broadcaster.ID, couple.Broadcaster);
            _stateObjects.Insert(couple.Receiver.ID, couple.Receiver);
            couple.Broadcaster.Subscribe("isActive", (state, prev) => couple.Receiver.ChangeObjectState("isActive", true));
        }

        StartMonitor();
    }
}

[Serializable]
public struct CoupleInteractables
{
    public StateObject Broadcaster;
    public StateObject Receiver;
}