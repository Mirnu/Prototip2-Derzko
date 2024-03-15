using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnabledState : FlashlightState
{
    public UnabledState(FlashlightStateMachine flashlightStateMachine) : base(flashlightStateMachine)
    {
    }

    public override bool Enter()
    {
        Flashlight.gameObject.GetComponent<Light2D>().intensity = 0;
        Debug.Log("Unable");
        return true;
    }

    public override bool Exit()
    {
        return base.Exit();
    }
}
