using System.Collections.Generic;
using UnityEngine;

public enum TransitionState
{
    Include,
    Exclude,
    All
}

public abstract class MovementState : State
{
    protected CharacterStateMachine CharacterStateMachine;
    protected Character Character;
    protected Transform transform => Character.transform;
    protected Vector3 Position => transform.position;
    protected Rigidbody2D Rigidbody => Character.Rigidbody;

    protected IHandler Handler;

    public TransitionState TransitionState = TransitionState.Include;
    public List<MovementState> PossibleTransitions = new List<MovementState>();

    public MovementState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
        CharacterStateMachine = characterStateMachine;
        Character = characterStateMachine.Character;
        Handler = characterStateMachine.Handler;
    }
}
