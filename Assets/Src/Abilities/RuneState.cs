using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneState : State
{
    protected RuneStateMachine CharacterStateMachine;

    protected IHandler Handler;

    public RuneState(RuneStateMachine characterStateMachine) : base(characterStateMachine)
    {
        CharacterStateMachine = characterStateMachine;
        Handler = characterStateMachine.Handler;
    }
}