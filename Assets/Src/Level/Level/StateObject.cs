using System;
using UnityEngine;

public abstract class StateObject : MonoBehaviour
{
    public abstract dynamic ?State { get; protected set; }

    public Action StateObjectChanged;

    public void ChangeObjectState(object state)
    {
        if (state.GetType() != (State as object).GetType()) return;

        State = state;
        StateObjectChanged?.Invoke();
    }
}
