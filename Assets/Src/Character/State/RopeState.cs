using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopeState : MovementState
{
    private float _ropeSwingSpeedBoost = 1f;

    private HingeJoint2D ropeBit;
    private const string ropeLayer = "Rope";

    public RopeState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) 
    {
        TransitionState = TransitionState.Exclude;
        PossibleTransitions.Add(CharacterStateMachine.DuckState);
    }

    public override bool Enter()
    {
        Character.CollisionEnter += OnCollisionEnter;
        return true;
    }

    public override bool Exit()
    {
        Handler.PressedKey -= KeyDown;
        Handler.PressedKeyUp -= KeyUp;
        Character.CollisionEnter -= OnCollisionEnter;
        return true;
    }

    private void OnCollisionEnter(Collision2D collision2D) 
    {
        if(collision2D.collider.gameObject.layer == LayerMask.NameToLayer(ropeLayer)) {
            ropeBit = collision2D.collider.transform.parent.GetComponentsInChildren<HingeJoint2D>().Last();
            Handler.PressedKey += KeyDown;
        } else {
            Character._characterStateMachine.ChangeState(Character._characterStateMachine.IdleState);
        }
    }
    
    private void KeyUp(KeyCode keyCode) {
        if(keyCode != KeyCode.W) return;
        if(Character.PlayerRopeHingeJoint.connectedBody != Character.Rigidbody) Character.PlayerRopeHingeJoint.connectedBody = Character.Rigidbody;
        Character._characterStateMachine.ChangeState(Character._characterStateMachine.IdleState);
    }

    private void KeyDown(KeyCode keyCode) {
        if(keyCode != KeyCode.W) return;
        if(Character.PlayerRopeHingeJoint.connectedBody != ropeBit.GetComponent<Rigidbody2D>()) {Character.PlayerRopeHingeJoint.connectedBody = ropeBit.GetComponent<Rigidbody2D>();}
        Character.Rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * _ropeSwingSpeedBoost, 0));
        if(Input.GetAxis("Horizontal")!=0 && _ropeSwingSpeedBoost < 10) {
            Debug.Log("boost: "+ _ropeSwingSpeedBoost);
            _ropeSwingSpeedBoost += 0.25f;
        }
        Handler.PressedKeyUp += KeyUp;
    }
}
