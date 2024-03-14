using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DimensionStateMachine : StateMachine, IInitializable
{
    public State CurrentDimensionState { get; private set; }
    public State LastState { get; private set; }
    public new IHandler Handler;

    [SerializeField] private List<State> availableDimensions = new List<State>();

    public override bool ChangeState(State newState) {
        bool entered = newState.Enter();
        if (!entered) return false;
        CurrentDimensionState.Exit();
        LastState = CurrentDimensionState;
        CurrentDimensionState = newState;
        return true;
    }

    public override void Initialize(State StartState)
    {
        CurrentDimensionState = StartState;
        CurrentDimensionState.Enter();
    }

    public void Initialize()
    {
        Initialize(new NavDimensionState(this));
    }
}
