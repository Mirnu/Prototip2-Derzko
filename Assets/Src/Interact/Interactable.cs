using System;
using UnityEngine;
using Zenject;

public abstract class Interactable : MonoBehaviour {
    
    public Action onInteractAction;
    public Action onInteractStart;
    public Action onInteractEnd;
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