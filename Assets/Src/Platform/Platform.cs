using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public List<MoveData> PlatformMovements;

    private const string playerLayer = "Player";
    private Action MovePlatformAction;
    
    private float timer = 0;
    private MoveData currentMovementData => PlatformMovements[0];
    private Vector3 target;
    private bool hasMoved = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) != playerLayer || hasMoved) return;
        hasMoved = true;
        target = transform.position + currentMovementData.PositionChange;
        MovePlatformAction += MovePlatform;
    }

    private void Update() => MovePlatformAction?.Invoke();

    private void MovePlatform() {
        if(Vector2.Distance(a: transform.position, currentMovementData.PositionChange) <= 0.001f) { 
            MovePlatformAction -= MovePlatform; 
        }
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target, timer/currentMovementData.moveTime);
        if(currentMovementData.NewRotation.x != 0 || currentMovementData.NewRotation.y != 0 || currentMovementData.NewRotation.z != 0 || currentMovementData.NewRotation.w != 0) {
            transform.rotation = Quaternion.Lerp(transform.rotation, currentMovementData.NewRotation, timer/currentMovementData.moveTime);
        }
    }
}