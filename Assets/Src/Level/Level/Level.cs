using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : LevelState
{
    [SerializeField] protected List<CoupleInteractables> coupleInteractables;

    private void Start()
    {
        initInteractables();
        StartMonitor();
    }

    private void initInteractables()
    {
        foreach (var couple in coupleInteractables)
        {
            _stateObjects.Add(couple.Broadcaster);
            _stateObjects.Add(couple.Receiver);
            maid.GiveTask(
                couple.Broadcaster.Subscribe("isActive", (state, prev) =>
                couple.Receiver.ChangeObjectState("isActive", state)));
        }
    }
}

[Serializable]
public struct CoupleInteractables
{
    public StateObject Broadcaster;
    public StateObject Receiver;
}