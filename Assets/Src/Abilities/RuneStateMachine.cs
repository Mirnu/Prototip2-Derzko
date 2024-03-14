using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RuneStateMachine : StateMachine, IInitializable
{
    public RuneState CurrentDimensionState { get; private set; }
    public RuneState LastState { get; private set; }
    public new IHandler Handler;

    public void Initialize()
    {
        
    }
}