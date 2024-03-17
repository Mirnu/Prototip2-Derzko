using System.Collections;
using UnityEngine;
using Zenject;

public class Player
{
    private Holdable currentItem;
    private bool isPrinting = false;
    private IHandler Handler;
    private Character _character;
    private AsyncProcessor _async;


    [Inject]
    public void Construct(IHandler handler, Character character, AsyncProcessor async)
    {
        Handler = handler;
        _character = character;
        _async = async;
    }

    private void KeyDown(KeyCode key) {
        if (key != KeyCode.E || currentItem == null) return;
        PlaceDown();
    }

    public void PickUp(Holdable item) {
        if (currentItem != null) return;
        Handler.PressedKeyDown += KeyDown;
        item.OnPickedUp();
        item.ItemPrefab.transform.SetParent(_character.heldItemPivot);
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
            _async.StartCoroutine(PrintText(obj.Text, obj.TextPopupSpeed));
        }
        isPrinting = false;
    }

    public IEnumerator PrintText(string textToPrint, float waitTime) {
        isPrinting = true;
        _character.text.text = "";
        for (int i = 0; i < textToPrint.Length; i++) {
            _character.text.text += textToPrint[i];
            yield return new WaitForSeconds(waitTime);
        }
    }
}