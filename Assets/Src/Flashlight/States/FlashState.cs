using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashState : FlashlightState
{
    public FlashState(FlashlightStateMachine flashlightStateMachine) : base(flashlightStateMachine)
    {
    }

    public override bool Enter()
    {
        Flashlight.gameObject.GetComponent<Light2D>().intensity = 1;
        Flashlight.gameObject.GetComponent<Light2D>().color = new Color(255, 255, 255);
        return true;
    }

    public override bool Exit()
    {
        FlashlightStateMachine.ChangeState(new UnabledState(FlashlightStateMachine));
        return true;
    }
}
