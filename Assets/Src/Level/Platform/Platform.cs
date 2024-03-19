using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

public class Platform : StateObject
{
    [Header("Список перемещений платформы")] public List<MoveData> PlatformMovements;
    
    [Header("Двигается ли платформа сама")][SerializeField] private bool isAutomatic = false;

    public override Dictionary<string, object> State { get; protected set; } = new Dictionary<string, object>() 
    {
        {"isActive", false}
    };

    private void Awake()
    {
        Subscribe("isActive", (active, prev) =>
        {
            if (!(bool)active) return;
            Move();
        });
    }

    private void Start() {
        if(isAutomatic) {
            Move();
        }
    }

    public void Move() => StartCoroutine(IterateOverMovements());

    public void StopMoving() => StopCoroutine(IterateOverMovements());

    public IEnumerator IterateOverMovements() {
        for(var i = 0; i < PlatformMovements.Count; i++) {
            if(PlatformMovements[i].loop) {
                StartCoroutine(MovePlatformCoroutine(PlatformMovements[i]));
                break;
            }
            yield return StartCoroutine(MovePlatformCoroutine(PlatformMovements[i]));
        }
    }

    private IEnumerator MovePlatformCoroutine(MoveData data) {
        Vector3 target = transform.position + data.PositionChange;
        for(float i = 0; i < data.moveTime; i += Time.deltaTime) {
            transform.position = Vector3.Lerp(transform.position, target, i/(data.moveTime*100));
            //Костыль чтобы еррор не вылезал
            if(data.NewRotation.x != 0 || 
                data.NewRotation.y != 0 || 
                data.NewRotation.z != 0 || 
                data.NewRotation.w != 0) {
                transform.rotation = Quaternion.Lerp(transform.rotation, data.NewRotation, i/(data.moveTime*100));
            }
            yield return new WaitForEndOfFrame();
        }
        if(data.loop) 
        {
            yield return new WaitForSeconds(data.loopWaitTime);
            StartCoroutine(MovePlatformCoroutine(new MoveData(-data.PositionChange, Quaternion.Euler(-data.NewRotation.eulerAngles), data.moveTime, data.loop)));
        }
    }
    private void OnDrawGizmos() {
        if(PlatformMovements.IsEmpty()) return;
        Vector3 sum = Vector3.zero;
        foreach(var data in PlatformMovements) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + sum, transform.position + sum + data.PositionChange);
            sum += data.PositionChange;
            if(data.loop) return;
        }
    }
}