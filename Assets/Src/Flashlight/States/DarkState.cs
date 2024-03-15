using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DarkState : FlashlightState
{
    public DarkState(FlashlightStateMachine flashlightStateMachine) : base(flashlightStateMachine)
    {
    }

    public override bool Enter()
    {
        Flashlight.gameObject.GetComponent<Light2D>().intensity = 1;
        Flashlight.gameObject.GetComponent<Light2D>().color = new Color(0, 0, 0);
        Debug.Log("Dark");
        return true;
    }

 /*   public override bool Exit()
    {
        FlashlightStateMachine.ChangeState(new UnabledState(FlashlightStateMachine));
        return true;
    }*/
}

