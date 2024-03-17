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
    public Light2D Light {get; private set;}

    public bool isActive = true;

    [Inject]
    public void Construct(IHandler handler)
    {
        _handler = handler;
        Light = GetComponent<Light2D>();
        _flashlightStateMachine = new FlashlightStateMachine(this, _handler);
        _flashlightStateMachine.Initialize();
    }

    private void Awake()
    {
        Debug.Log(_handler);
        _handler.PressedKeyDown += KeyDown;
    }

    private bool isWorking = false;
    private void KeyDown(KeyCode key)
    {
        if (key != KeyCode.F) return;
        if (!isActive) return;
        if (isWorking)
            _flashlightStateMachine.ChangeState(_flashlightStateMachine.unabledState);
        else
            _flashlightStateMachine.ChangeState(_flashlightStateMachine.LastState);
        isWorking = !isWorking;
    }

    private void ExecuteFlashlightFunctions() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Light.pointLightOuterRadius);
        foreach (var col in colliders){
            IOnFlashlightAction func;
            if (col.gameObject.TryGetComponent<IOnFlashlightAction>(out func)){
                func?.OnFlashlightAction.Invoke();
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
