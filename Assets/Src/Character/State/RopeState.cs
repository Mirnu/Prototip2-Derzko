using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopeState : MovementState
{
    private float _ropeSwingSpeedBoost = 1f;

    public bool canGrab = false;

    private const string ropeLayer = "Rope";

    public RopeState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) 
    {
        TransitionState = TransitionState.Exclude;
        PossibleTransitions.Add(CharacterStateMachine.DuckState);
    }

    public override bool Enter()
    {
        if(!canGrab) return false;
        Handler.PressedKey += KeyDown;
        Handler.PressedKeyUp += KeyUp;
        return true;
    }

    public override bool Exit()
    {
        Handler.PressedKey -= KeyDown;
        Handler.PressedKeyUp -= KeyUp;
        return true;
    }
    
    private void KeyUp(KeyCode keyCode) {
        Character.Rigidbody.velocity = (Character.transform.right + Character.transform.up) * (_ropeSwingSpeedBoost/5);
        Character.PlayerRopeHingeJoint.connectedBody = Character.Rigidbody;
        Character._characterStateMachine.ChangeState(Character._characterStateMachine.IdleState);
    }

    private void KeyDown(KeyCode keyCode) {
        Character.Rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * _ropeSwingSpeedBoost, Input.GetAxis("Horizontal")/2));
        if(Input.GetAxis("Horizontal") != 0 && _ropeSwingSpeedBoost < 10) {
            _ropeSwingSpeedBoost += 0.25f;
        }
    }
}
