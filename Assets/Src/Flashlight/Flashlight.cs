using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Zenject;

public class Flashlight : MonoBehaviour
{
    private FlashlightStateMachine _flashlightStateMachine;
    private IHandler _handler;

    [Inject]
    public void Construct(IHandler handler)
    {
        _handler = handler;
        _flashlightStateMachine = new FlashlightStateMachine(this, _handler);
        _flashlightStateMachine.Initialize();
    }

    private void Awake()
    {
        Debug.Log(_handler);
        _handler.PressedKeyDown += KeyDown;
    }

    private bool isFlash = true;
    private bool isActive = false;
    private void KeyDown(KeyCode key)
    {
        if (key == KeyCode.F)
        {
            if (isActive) _flashlightStateMachine.ChangeState(_flashlightStateMachine.unabledState);
            else _flashlightStateMachine.ChangeState(_flashlightStateMachine.flashState);
            isActive = !isActive;
        }
        else if (key == KeyCode.Q)
        {
            if (isActive)
            {
                if (isFlash) _flashlightStateMachine.ChangeState(_flashlightStateMachine.darkState);
                else _flashlightStateMachine.ChangeState(_flashlightStateMachine.flashState);
                isFlash = !isFlash;
            }         
        }
    }

    private void Update() {
        _flashlightStateMachine.Tick();
    }

    private void OnDestroy()
    {
        _handler.PressedKeyDown -= KeyDown;
    }
}
