using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TextMeshProUGUI text;

    public Transform heldItemPivot;

    private Holdable currentItem;
    private bool isPrinting = false;
    private IHandler Handler;

    [Inject]
    public void Construct(IHandler handler)
    {
        Handler = handler;
    }

    private void KeyDown(KeyCode key) {
        if (key != KeyCode.E || currentItem == null) return;
        PlaceDown();
    }

    public void PickUp(Holdable item) {
        if (currentItem != null) return;
        Handler.PressedKeyDown += KeyDown;
        item.OnPickedUp();
        item.ItemPrefab.transform.SetParent(heldItemPivot);
        item.ItemPrefab.transform.localPosition = new Vector3(0, 0, 0);
        currentItem = item;
    }

    public void PlaceDown() {
        Handler.PressedKeyDown -= KeyDown;
        currentItem.OnPlacedDown();
        currentItem.ItemPrefab.transform.SetParent(null);
        currentItem = null;
    }

    public void PrintHeadText(TextScriptableObject obj) {
        if(!isPrinting) {
            StartCoroutine(PrintText(obj.Text, obj.TextPopupSpeed));
        }
        isPrinting = false;
    }

    public IEnumerator PrintText(string textToPrint, float waitTime) {
        isPrinting = true;
        text.text = "";
        for (int i = 0; i < textToPrint.Length; i++) {
            text.text += textToPrint[i];
            yield return new WaitForSeconds(waitTime);
        }
    }
}