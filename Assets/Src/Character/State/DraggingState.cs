using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingState : MovementState
{
    private float _speed = 0.7f;

    public DraggingState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        TransitionState = TransitionState.Include;
        PossibleTransitions.Add(characterStateMachine.IdleState);
    }

    public override bool Enter()
    {
        Character.Speed = _speed;
        return true;
    }
}
