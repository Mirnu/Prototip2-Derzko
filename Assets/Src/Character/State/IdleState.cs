using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementState
{

    public IdleState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) 
    {
        TransitionState = TransitionState.All;
    }

    public override bool Enter()
    {
        Handler.PressedKey += KeyDown;
        return true;
    }

    public override bool Exit()
    {
        Handler.PressedKey -= KeyDown;
        return true;
    }

    private void KeyDown(KeyCode key)
    {
        if (key == KeyCode.Space)
        {
            CharacterStateMachine.ChangeState(CharacterStateMachine.JumpState);
            return;
        }

        CharacterStateMachine.ChangeState(CharacterStateMachine.WalkState);
    }
}
