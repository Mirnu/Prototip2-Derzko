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


    private bool isPrinting = false;

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