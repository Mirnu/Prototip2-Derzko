using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class StateObject : MonoBehaviour
{
    public virtual Dictionary<string, object> ?State { get; protected set; }
    public Dictionary<string, object> PrevState { get; protected set; }

    public Action ObjectStateChanged; 

    private Dictionary<string, List<Action>> _subscribers = new Dictionary<string, List<Action>>();

    public void ChangeObjectState(string state, object newValue)
    {
        if (!State.ContainsKey(state)) throw new Exception("Property is null");

        PrevState = State;
        State[state] = newValue;
        ObjectStateChanged?.Invoke();
        callSubscribes(state);
    }

    private void callSubscribes(string newValue)
    {
        foreach (var subscriber in _subscribers[newValue])
        {
            subscriber?.Invoke();
        }
    }

    public Action Subscribe(string name, Action action)
    {
        if (!_subscribers.ContainsKey(name))
            _subscribers[name] = new List<Action>();
        
        _subscribers[name].Add(action);

        return () => _subscribers[name].Remove(action);
    }
}
