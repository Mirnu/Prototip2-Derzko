using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MoveData", menuName = "Data/PlatformMoveData", order = 1)]
public class MoveData : ScriptableObject {
    [Tooltip("На сколько переместится платформа")]
    public Vector3 PositionChange;
    [Tooltip("Конечное вращение платформы")]
    public Quaternion NewRotation;
    [Tooltip("Время на перемещение платформы в милисекундах(вроде)")]
    public float moveTime;
    [Tooltip("Ходит ли платформа 'кругами'")]
    public bool loop = false;
}