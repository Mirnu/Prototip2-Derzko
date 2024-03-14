using System;
using System.Collections.Generic;
using UnityEngine;

public class Lever : KeyInteractable {
    
    [SerializeField] private List<Sprite> leverStates = new List<Sprite>(); 

    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onInteractAction += delegate { 
            FindObjectOfType<Player>().PrintHeadText("Lorem Impsum", 2f); 
        };
    }
    void log() {
        Debug.Log("Interacted /w: " + name);
    }
}