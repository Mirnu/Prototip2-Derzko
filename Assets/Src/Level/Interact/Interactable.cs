using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : StateObject
{

    public Action onInteractAction;
    public Action onInteractStart;
    public Action onInteractEnd;
    public InteractableState state = new();

    public IHandler Handler;

    protected Player player;


    [Inject]
    public void Construct(IHandler handler, Player player)
    {
        Handler = handler;
        this.player = player;
    }

    public void InteractionStart()
    {
        onInteractStart?.Invoke();
    }

    public void InteractEnd()
    {
        onInteractEnd?.Invoke();
    }
}

public abstract class Interactor : MonoBehaviour
{
    public abstract void Interact();
}
public struct InteractableState
{
    public bool isActive;
}