using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

[System.Serializable]
public class CharacterStateMachine : StateMachine, ITickable, IInitializable
{
    public Character Character { get; private set; }

    public MovementState CurrentState { get; private set; }
    public MovementState LastState { get; private set; }

    public IdleState IdleState { get; private set; }
    public RunState RunState { get; private set; }
    public WalkState WalkState { get; private set; }
    public JumpState JumpState { get; private set; }
    public DuckState DuckState {  get; private set; }
    public StairState StairState { get; private set; }

    public new IHandler Handler { get; private set; }


    public CharacterStateMachine(Character character, IHandler handler)
    {
        Character = character;
        Handler = handler;
        IdleState = new IdleState(this);
        RunState = new RunState(this);
        WalkState = new WalkState(this);
        JumpState = new JumpState(this);
        DuckState = new DuckState(this);
        StairState = new StairState(this);
    }

    public void Initialize() => Initialize(IdleState);
    
    public void Tick() => CurrentState.Update();

    public void Initialize(MovementState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public bool ChangeState(MovementState newState)
    {
        if (!checkOnPossibilityChanged(newState)) return false;
        if (!newState.Enter())
        {
            CurrentState.Enter();
            return false;
        }
        LastState = CurrentState;
        CurrentState = newState;
        Debug.Log($"Current new State: {CurrentState}");
        return true;
    }

    private bool checkOnPossibilityChanged(MovementState newState)
    {
        if (!checkOnTransition(newState)) return false;
        return  newState != CurrentState &&
            CurrentState.Exit();
    }

    private bool checkOnTransition(MovementState newState)
    {
        if (CurrentState.TransitionState == TransitionState.Include &&
            !CurrentState.PossibleTransitions.Contains(newState)) return false;
        else if (CurrentState.TransitionState == TransitionState.Exclude &&
            CurrentState.PossibleTransitions.Contains(newState)) return false;
        
        return true;
    }
}
