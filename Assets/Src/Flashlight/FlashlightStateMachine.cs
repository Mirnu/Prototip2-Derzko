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

    public FlashlightState LastState { get; private set; }
    public FlashlightState CurrentState { get; private set; }

    public new IHandler Handler { get; private set; }
    public FlashlightStateMachine(Flashlight flashlight, IHandler handler)
    {
        Flashlight = flashlight;
        Handler = handler;
        unabledState = new UnabledState(this);
        flashState = new FlashState(this);
        darkState = new DarkState(this);
    }

    public void Initialize() => Initialize(unabledState);

    public void Tick() => CurrentState.Update();

    public void Initialize(FlashlightState flashlightState)
    {
        LastState = flashState;
        CurrentState = flashlightState;
        CurrentState.Enter();
    }

    public bool ChangeState(FlashlightState newState)
    {
        if (newState == CurrentState) return false;
        if (!Flashlight.isActive){
            CurrentState = unabledState;
            return false;
        }
        if (!CurrentState.Exit()) return false;
        if (!newState.Enter())
        {
            CurrentState.Enter();
            return false;
        }
        if (CurrentState != unabledState)
            LastState = CurrentState;
        CurrentState = newState;
        Debug.Log($"Current new State: {CurrentState}");
        return true;
    }
}
