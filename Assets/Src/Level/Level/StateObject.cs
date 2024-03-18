using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class StateObject : MonoBehaviour
{
    public virtual Dictionary<string, object> State { get; protected set; }
    public Dictionary<string, object> PrevState { get; protected set; }

    public Action<int, Dictionary<string, object>> ObjectStateChanged; 

    private Dictionary<string, List<Action<object, object>>> _subscribers = new Dictionary<string, List<Action<object, object>>>();

    public void ChangeObjectState(string state, object newValue)
    {
        if (!State.ContainsKey(state)) throw new Exception("Property is null");
        if (State[state] == newValue) return;
        PrevState = State;
        State[state] = newValue;
        ObjectStateChanged?.Invoke(GetInstanceID(), PrevState);
        callSubscribes(state);
    }

    private void callSubscribes(string newValue)
    {
        foreach (var subscriber in _subscribers[newValue])
        {
            subscriber?.Invoke(State[newValue], PrevState[newValue]);
        }
    }

    public Action Subscribe(string name, Action<object, object> action)
    {
        if (!_subscribers.ContainsKey(name))
            _subscribers[name] = new List<Action<object, object>>();
        
        _subscribers[name].Add(action);

        return () => _subscribers[name].Remove(action);
    }
}
