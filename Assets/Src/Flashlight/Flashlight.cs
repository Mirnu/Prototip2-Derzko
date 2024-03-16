using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class Flashlight : MonoBehaviour
{
    private FlashlightStateMachine _flashlightStateMachine;
    private IHandler _handler;
    private Light2D _light;

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
        _light = GetComponent<Light2D>();
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

    private void ExecuteFlashlightFunctions() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _light.pointLightOuterRadius);
        foreach (var col in colliders){
            IOnFlashlight func;
            if (col.gameObject.TryGetComponent<IOnFlashlight>(out func)){
                if (isFlash)
                    func?.OnFlashlight();
                else
                    func?.OnDarklight();
            }
        }
    }

    private void Update() {
        ExecuteFlashlightFunctions();
        _flashlightStateMachine.Tick();
    }

    private void OnDestroy()
    {
        _handler.PressedKeyDown -= KeyDown;
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, GetComponent<Light2D>().pointLightOuterRadius);
    // }
}
