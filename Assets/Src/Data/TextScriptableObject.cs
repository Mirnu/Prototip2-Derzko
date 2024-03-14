using UnityEngine;

[CreateAssetMenu(fileName = "TextScriptableObject", menuName = "Data/TextScriptableObject", order = 0)]
public class TextScriptableObject : ScriptableObject {
    public string Text;
    public float TextPopupSpeed = 1f;
}