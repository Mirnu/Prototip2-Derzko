using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DarkState : FlashlightState
{
    public DarkState(FlashlightStateMachine flashlightStateMachine) : base(flashlightStateMachine)
    {
    }

    public override bool Enter()
    {
        Flashlight.Light.intensity = 1;
        Flashlight.Light.color = new Color(0, 0, 0);
        Handler.PressedKeyDown += KeyDown;
        Flashlight.ColliderEnabled = true;
        return true;
    }

     private void KeyDown(KeyCode key){
        if (key != KeyCode.Q) return;
        FlashlightStateMachine.ChangeState(FlashlightStateMachine.flashState);
    }

    public override bool Exit()
    {
        Handler.PressedKeyDown -= KeyDown;
        return true;
    }
}

