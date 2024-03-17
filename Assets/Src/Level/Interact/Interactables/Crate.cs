using System;
using System.Collections.Generic;
using UnityEngine;

public class Crate : KeyInteractable, Holdable {

    private SpriteRenderer _spriteRenderer;

    GameObject Holdable.ItemPrefab { get => gameObject; }
    public override dynamic State { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public void OnPickedUp() {
        Debug.Log("PickedUp: " + name);
    }

    public void OnPlacedDown() {
        Debug.Log("PlacedDown: " + name);
    }

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onInteractAction += delegate { 
            player.PickUp(this);
        };
    }

    void log() {
        Debug.Log("Interacted /w: " + name);
    }
}