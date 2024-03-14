using System;
using System.Collections.Generic;
using UnityEngine;

public class Lever : KeyInteractable {
    
    [SerializeField] private List<Sprite> leverStates = new List<Sprite>(); 

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onInteractAction += log;
    }
    void log() {
        Debug.Log("Interacted /w: " + name);
    }
}