using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
            _stateObjects.Insert(couple.Broadcaster.ID, couple.Broadcaster);
            _stateObjects.Insert(couple.Receiver.ID, couple.Receiver);
            maid.GiveTask(
                couple.Broadcaster.Subscribe("isActive", (state, prev) =>
                couple.Receiver.ChangeObjectState("isActive", true)));
        }
    }
}

[Serializable]
public struct CoupleInteractables
{
    public StateObject Broadcaster;
    public StateObject Receiver;
}