using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : Interactable
{
    public override Dictionary<string, object> State { get; protected set; } = new Dictionary<string, object>()
    {
        {"isActive", false}
    };

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
        Debug.Log(!(bool)State["isActive"]);
        ChangeObjectState("isActive", !(bool)State["isActive"]);
    }
}