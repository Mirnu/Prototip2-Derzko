using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DimensionState : State
{
    protected DimensionStateMachine _stateMachine;
    protected IHandler Handler;

    public DimensionState(DimensionStateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        Handler = stateMachine.Handler;
    }
}
