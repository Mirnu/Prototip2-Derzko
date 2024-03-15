using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlashlightState : State
{
    protected FlashlightStateMachine FlashlightStateMachine;
    protected Flashlight Flashlight;

    protected IHandler Handler;

    public FlashlightState(FlashlightStateMachine flashlightStateMachine) : base(flashlightStateMachine)
    {
        FlashlightStateMachine = flashlightStateMachine;
        Flashlight = flashlightStateMachine.Flashlight;
        Handler = flashlightStateMachine.Handler;
    }

}
