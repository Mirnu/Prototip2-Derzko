using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDimensionState : State {

    public NavDimensionState(DimensionStateMachine dimensionStateMachine) : base(dimensionStateMachine) { }
    
    public override bool Enter() {
        Debug.Log("Entered(NavDimensionState): " + this);
        return true;
    }

}