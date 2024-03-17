using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MoveData", menuName = "Data/PlatformMoveData", order = 1)]
public class MoveData : ScriptableObject {
    [Header("На сколько переместится платформа")]
    public Vector3 PositionChange;
    [Header("Конечное вращение платформы")]
    public Quaternion NewRotation;
    [Header("Время на перемещение платформы")]
    public float moveTime;
    [Header("Ходит ли платформа 'кругами'")]
    public bool loop = false;
    [HideInInspector]
    public float loopWaitTime;

    public MoveData(Vector3 delta, Quaternion newRot, float time, bool loop) {
        PositionChange = delta;
        NewRotation = newRot;
        moveTime = time;
        this.loop = loop;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(MoveData))]
public class MoveDataEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector(); // for other non-HideInInspector fields
		MoveData script = (MoveData)target;
		if (script.loop) // if bool is true, show other fields
		{
			GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("На сколько секунд платформа останавливается перед разворотом");
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("LoopWaitTime", GUILayout.Width(122));
            script.loopWaitTime = EditorGUILayout.FloatField(script.loopWaitTime);
            GUILayout.EndHorizontal();
		}
	}
}
#endif