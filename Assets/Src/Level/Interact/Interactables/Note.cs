using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Note : KeyInteractable {
    
    [SerializeField] private List<Sprite> leverStates = new List<Sprite>(); 
    [SerializeField] private TextScriptableObject text;

    private TextMeshProUGUI _text;
    private SpriteRenderer _spriteRenderer;
    private bool isPrinting = false;

    private void Start() {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = "";
        _spriteRenderer = GetComponent<SpriteRenderer>();
        onInteractAction += delegate { 
            if(isPrinting) return;
            StartCoroutine(PrintText(text.Text, text.TextPopupSpeed));
        };
    }

    public IEnumerator PrintText(string textToPrint, float waitTime) {
        isPrinting = true;
        _text.text = "";
        for (int i = 0; i < textToPrint.Length; i++) {
            _text.text += textToPrint[i];
            yield return new WaitForSeconds(waitTime);
        }
    }
}