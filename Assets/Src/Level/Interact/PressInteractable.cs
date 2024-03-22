using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PressInteractable : Interactable
{
    public override Dictionary<string, object> State { get; protected set; } = new Dictionary<string, object>()
    {
        {"isActive", false}
    };

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out Character character) || other.TryGetComponent(out Crate crate)) {
            ChangeObjectState("isActive", true);
            onInteractAction?.Invoke();
        }
    }
}