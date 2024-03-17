using System;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

public class KeyInteractable : Interactable
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out Character character)) {
            Handler.PressedKeyDown += KeyDown;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.TryGetComponent(out Character character)) {
            Handler.PressedKeyDown -= KeyDown;
        }
    }

    private void KeyDown(KeyCode key) {
        if (key != KeyCode.E) return;
        onInteractAction?.Invoke();
        InteractEnd();

        //Чтобы взаимодествия несколько раз не вызывались
        Handler.PressedKeyDown -= KeyDown;
    }
}