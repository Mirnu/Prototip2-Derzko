using System;
using UnityEngine;
using Zenject;

public abstract class PressInteractable : Interactable
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out Character character) || other.TryGetComponent(out Crate crate)) {
            InteractionStart();
            onInteractAction?.Invoke();
            InteractEnd();
        }
    }
}