using System;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public MoveData PlatformMoveData;

    private const string playerLayer = "Player";
    private Action MovePlatformAction;
    
    private float timer = 0;

    private void OnCollisionEnter2D(Collision2D other) {
        if(LayerMask.LayerToName(other.gameObject.layer) != playerLayer) return;
        MovePlatformAction += MovePlatform;
    }

    private void Update() => MovePlatformAction?.Invoke();

    private void MovePlatform() {
        Debug.Log("MovePlatform");
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, transform.position + PlatformMoveData.PositionChange, timer/PlatformMoveData.moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, PlatformMoveData.NewRotation, timer/PlatformMoveData.moveTime);
    }

}
[CreateAssetMenu(fileName = "MoveData", menuName = "Data/PlatformMoveData", order = 1)]
public class MoveData : ScriptableObject {
    public Vector3 PositionChange;
    public Quaternion NewRotation;
    public float moveTime;
}