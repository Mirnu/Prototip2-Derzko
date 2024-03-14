using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;
    protected IHandler Handler;

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        Handler = stateMachine.Handler;
    }

    public virtual bool Enter() => true;
    public virtual bool Exit() => true;
    public virtual void Update() {}
}
