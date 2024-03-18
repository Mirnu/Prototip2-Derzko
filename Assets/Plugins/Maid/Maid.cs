using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maid
{
    private List<Action> actions = new List<Action>();
    public void GiveTask(Action action) => actions.Add(action);

    public void Clear() 
    {
        foreach (var action in actions)
        {
            action();
        }
    }
}
