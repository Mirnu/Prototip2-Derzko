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

    private int _colliders = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out Character character) || other.TryGetComponent(out Crate crate)) {
            _colliders++;
            ChangeObjectState("isActive", true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character) || collision.TryGetComponent(out Crate crate))
        {
            _colliders--;
            if (_colliders == 0) 
                ChangeObjectState("isActive", false);
        }
    }
}