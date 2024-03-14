using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DuckState : MovementState
{
    public DuckState(CharacterStateMachine characterStateMachine) : base(characterStateMachine) {}

    private float _speed = 0.5f;

    public override bool Enter()
    {
        Character.Speed = _speed;
        Character.Collider.size /= new Vector2(1, 2);
        Character.Collider.offset -= new Vector2(0, Character.Collider.size.y / 2);
        Handler.PressedKeyUp += KeyUp;
        return true;
    }

    public override bool Exit()
    {
        if (checkOnExit()) return false;

        Character.Collider.offset += new Vector2(0, Character.Collider.size.y / 2);
        Character.Collider.size *= new Vector2(1, 2);
        Handler.PressedKeyUp -= KeyUp;
        return true;
    }

    private void KeyUp(KeyCode key)
    {
        if (key != KeyCode.LeftControl) return;
        CharacterStateMachine.ChangeState(CharacterStateMachine.IdleState);
    }

    private bool checkOnExit()
    {
        return Physics2D.OverlapCircleAll(Character.transform.position + new Vector3(0, 0.5f), 0.5f).Length > 1;
    }
}
