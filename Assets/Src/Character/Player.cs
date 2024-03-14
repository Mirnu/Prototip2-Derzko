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

    public void PrintHeadText(string textToPrint, float waitTime) {
        if(!isPrinting) {
            StartCoroutine(PrintText(textToPrint, waitTime));
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