using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour {
    
    public Action onInteractAction;
    public Action onInteractStart;
    public Action onInteractEnd;
    public InteractableState state;
    public IHandler Handler;

    [Inject]
    public void Construct(IHandler handler)
    {
        Handler = handler;
    }

    public void InteractionStart() {
        onInteractStart?.Invoke();
    }

    public void InteractEnd() {
        onInteractEnd?.Invoke();
    }
}
public struct InteractableState {
    public bool isActive;
}