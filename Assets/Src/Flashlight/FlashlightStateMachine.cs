using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class FlashlightStateMachine : StateMachine, ITickable, IInitializable
{
    public UnabledState unabledState { get; private set; }
    public FlashState flashState { get; private set; }
    public DarkState darkState { get; private set; }
    
    public Flashlight Flashlight { get; private set; }

    private FlashlightState LastState { get; set; }
    private FlashlightState CurrentState { get; set; }

    public new IHandler Handler { get; private set; }
    public FlashlightStateMachine(Flashlight flashlight, IHandler handler)
    {
    }

    public void Initialize() => Initialize(unabledState);

    public void Tick() => CurrentState.Update();

    public void Initialize(FlashlightState flashlightState)
    {
        CurrentState = flashlightState;
        CurrentState.Enter();
    }

    public bool ChangeState(FlashlightState newState)
    {
        if (newState == CurrentState) return false;
        if (!CurrentState.Exit()) return false;
        LastState = CurrentState;
        CurrentState = newState;
        return true;
    }

}
