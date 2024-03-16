using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementState
{
    private float _speed = 1.5f;
    
    public WalkState(CharacterStateMachine characterStateMachine) : 
        base(characterStateMachine) 
    {
        TransitionState = TransitionState.All;
    }

    public override bool Enter()
    {
        Handler.PressedKeyDown += KeyDown;
        Character.Speed = _speed;
        return true;
    }

    public override bool Exit() 
    {
        Handler.PressedKeyDown -= KeyDown;
        return true;
    }

    private void KeyDown(KeyCode keyCode)
    {
        if (keyCode != KeyCode.LeftShift) return;
        CharacterStateMachine.ChangeState(CharacterStateMachine.RunState);
    }
}
