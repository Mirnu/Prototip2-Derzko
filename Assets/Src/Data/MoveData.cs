using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MoveData", menuName = "Data/PlatformMoveData", order = 1)]
public class MoveData : ScriptableObject {
    [Header("На сколько переместится платформа")]
    public Vector3 PositionChange;
    [Header("Конечное вращение платформы")]
    public Quaternion NewRotation;
    [Header("Время на перемещение платформы в милисекундах(вроде)")]
    public float moveTime;
    [Header("Ходит ли платформа 'кругами'")]
    public bool loop = false;

    public MoveData(Vector3 delta, Quaternion newRot, float time, bool loop) {
        PositionChange = delta;
        NewRotation = newRot;
        moveTime = time;
        this.loop = loop;
    }
}