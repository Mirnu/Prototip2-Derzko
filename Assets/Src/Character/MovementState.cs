using UnityEngine;

public abstract class MovementState : State
{
    protected CharacterStateMachine CharacterStateMachine;
    protected Character Character;
    protected Rigidbody2D Rigidbody => Character.Rigidbody;

    protected IHandler Handler;

    public MovementState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        CharacterStateMachine = characterStateMachine;
        Character = characterStateMachine.Character;
        Handler = characterStateMachine.Handler;
    }
}
