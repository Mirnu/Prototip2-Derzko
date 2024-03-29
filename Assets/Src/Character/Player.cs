using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Player
{
    private Holdable currentItem;
    private IHandler Handler;
    public Character Character;
    private AsyncProcessor _async;

    [Inject]
    public void Construct(IHandler handler, Character character, AsyncProcessor async)
    {
        Handler = handler;
        Character = character;
        _async = async;
    }

    private void KeyDown(KeyCode key) {
        if (key != KeyCode.E) return;
        PlaceDown();
    }

    public void PickUp(Holdable item) {
        if (currentItem != null) return;
        Handler.PressedKeyDown += KeyDown;
        Character._characterStateMachine.ChangeState(Character._characterStateMachine.DraggingState);
        item.OnPickedUp();
        item.ItemPrefab.transform.SetParent(Character.heldItemPivot);
        item.ItemPrefab.transform.localPosition = new Vector3(0, 0, 0);
        currentItem = item;
    }

    public void PlaceDown() {
        Handler.PressedKeyDown -= KeyDown;
        Character._characterStateMachine.ChangeState(Character._characterStateMachine.IdleState);
        currentItem.OnPlacedDown();
        currentItem.ItemPrefab.transform.SetParent(null);
        currentItem = null;
    }
}