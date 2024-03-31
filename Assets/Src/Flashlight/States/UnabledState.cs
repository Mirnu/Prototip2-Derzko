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
        Flashlight.Light.intensity = 0;
        Flashlight.ColliderEnabled = false;
        return true;
    }
}
