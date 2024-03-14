using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDimensionState : State 
{
    public BlueDimensionState(DimensionStateMachine dimensionStateMachine) : base(dimensionStateMachine) { }
    
    public override bool Enter() {
        Debug.Log("Entered(BlueDimensionState): " + this);
        return true;
    }
}
