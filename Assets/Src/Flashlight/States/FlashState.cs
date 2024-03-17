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
        Flashlight.Light.intensity = 1;
        Flashlight.Light.color = new Color(255, 255, 255);
        Handler.PressedKeyDown += KeyDown;
        return true;
    }

    private void KeyDown(KeyCode key){
        if (key != KeyCode.Q) return;
        FlashlightStateMachine.ChangeState(FlashlightStateMachine.darkState);
    }

    public override bool Exit()
    {
        Handler.PressedKeyDown -= KeyDown;
        return true;
    }
}
