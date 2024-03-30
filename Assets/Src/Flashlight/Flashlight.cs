using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
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

    private CircleCollider2D _collider;
    public bool ColliderEnabled { get => _collider.enabled; set { _collider.enabled = value; } }

    [Inject]
    public void Construct(IHandler handler)
    {
        _handler = handler;
        Light = GetComponent<Light2D>();
        _flashlightStateMachine = new FlashlightStateMachine(this, _handler);
        _collider = gameObject.AddComponent<CircleCollider2D>();
        _collider.radius = Light.pointLightOuterRadius*2;
        _collider.isTrigger = true;
        _collider.enabled = false;
        _flashlightStateMachine.Initialize();
    }

    private void Awake()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IOnFlashlightAction func))
        {
            func?.OnFlashlightAction.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HiddenPlatform hiddenPlatform))
        {
            if (_flashlightStateMachine.CurrentState.ToString() == hiddenPlatform.PlatfomShowState)
                hiddenPlatform?.Show();
            else
                hiddenPlatform?.Hide();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HiddenPlatform hiddenPlatform))
        {
            hiddenPlatform?.Hide();
        }
    }

    private void Update() {
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
