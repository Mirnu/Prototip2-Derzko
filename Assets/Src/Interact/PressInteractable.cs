using System;
using UnityEngine;
using Zenject;

public class PressInteractable : Interactable
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out Character character)) {
            InteractionStart();
            onInteractAction?.Invoke();
            InteractEnd();
        }
    }
}