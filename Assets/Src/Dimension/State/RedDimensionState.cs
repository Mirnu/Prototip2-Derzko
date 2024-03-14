using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDimensionState : State
{
    
    public RedDimensionState(DimensionStateMachine dimensionStateMachine) : base(dimensionStateMachine) { }
    
    public override bool Enter() {
        Debug.Log("Entered(RedDimensionState): " + this);
        return true;
    }
}
