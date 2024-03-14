using UnityEngine;

public class RunState : MovementState
{
    private float _speed = 3;

    public RunState(CharacterStateMachine characterStateMachine) :
        base(characterStateMachine) { }

    public override bool Enter()
    {
        Character.Speed = _speed;
        Handler.PressedKeyUp += KeyUp;

        return true;
    }

    public override bool Exit()
    {
        Handler.PressedKeyUp -= KeyUp;
        return true;
    }

    private void KeyUp(KeyCode key) 
    {
        if (key != KeyCode.LeftShift) return;
        CharacterStateMachine.ChangeState(CharacterStateMachine.WalkState);
    }
}
