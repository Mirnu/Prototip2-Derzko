using System;
using System.Collections.Generic;
using UnityEngine;

public class Lever : KeyInteractable {
    
    [SerializeField] private List<Sprite> leverStates = new List<Sprite>(); 
    [SerializeField] private TextScriptableObject text;

    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onInteractAction += delegate { 
            FindObjectOfType<Player>().PrintHeadText(text); 
        };
    }

    void log() {
        Debug.Log("Interacted /w: " + name);
    }
}