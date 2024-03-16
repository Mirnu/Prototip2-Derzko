using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public List<MoveData> PlatformMovements;

    private const string playerLayer = "Player";
    private Action MovePlatformAction;
    
    private float timer = 0;
    private Vector3 target;
    private MoveData currentMovementData;

    private void OnCollisionEnter2D(Collision2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) != playerLayer) return;
        currentMovementData = PlatformMovements[0];
        target = transform.position + currentMovementData.PositionChange;
        MovePlatformAction += MovePlatform;
    }

    private void Update() => MovePlatformAction?.Invoke();

    private void MovePlatform() {
        if(Vector2.Distance(a: transform.position, target) <= 0.001f) { MovePlatformAction -= MovePlatform; return; }
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target, timer/currentMovementData.moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentMovementData.NewRotation, timer/currentMovementData.moveTime);
    }
}
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