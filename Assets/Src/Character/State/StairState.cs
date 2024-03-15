using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairState : MovementState
{
    public bool canRise = false;
    private float _stairSpeed = 2f;

    public StairState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) 
    {
        PossibleTransitions.Add(CharacterStateMachine.IdleState);
    }

    public override bool Enter()
    {
        if (!canRise) return false;
        Handler.PressedKey += KeyPressed;
        Character.Speed = 0;
        Rigidbody.simulated = false;

        return true;
    }

    public override bool Exit()
    {
        Handler.PressedKey -= KeyPressed;
        Rigidbody.simulated = true;
        return true;
    }

    private void KeyPressed(KeyCode keyCode)
    {
        Vector3 offset = Vector2.up * Input.GetAxisRaw("Vertical") * _stairSpeed * Time.deltaTime;
        Character.transform.Translate(offset);

        if (!checkOnStairDown()) 
            CharacterStateMachine.ChangeState(CharacterStateMachine.IdleState);
    }

    private bool checkOnStairDown()
    {
        Vector2 originDown = new Vector2(Position.x, Position.y - transform.localScale.y / 2);
        Vector2 originUp = new Vector2(Position.x, Position.y + transform.localScale.y / 2);
        return Physics2D.Raycast(originDown, Vector2.down, 0.01f) || Physics2D.Raycast(originUp, Vector2.up, 0.01f);
 
    }
}
