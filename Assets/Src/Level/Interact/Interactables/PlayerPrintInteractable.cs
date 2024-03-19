using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrintInteractable : KeyInteractable {
    
    [SerializeField] private List<Sprite> leverStates = new List<Sprite>(); 
    [SerializeField] private TextScriptableObject text;

    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onInteractAction += delegate { 
            player.PrintHeadText(text);
        };
    }
}