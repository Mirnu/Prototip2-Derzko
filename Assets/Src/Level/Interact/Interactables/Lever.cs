using System;
using System.Collections.Generic;
using UnityEngine;

public class Lever : KeyInteractable {
    
    [SerializeField] private List<Sprite> leverStates = new List<Sprite>(); 
    [SerializeField] private TextScriptableObject text;

    private SpriteRenderer _spriteRenderer;

    public override dynamic State { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onInteractAction += delegate { 
            player.PrintHeadText(text);
            
        };
    }

    void log() {
        Debug.Log("Interacted /w: " + name);
    }
}