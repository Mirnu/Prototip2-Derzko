using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : StateObject
{

    public Action onInteractAction;

    public IHandler Handler;

    protected Player player;


    [Inject]
    public void Construct(IHandler handler, Player player)
    {
        Handler = handler;
        this.player = player;
    }
}