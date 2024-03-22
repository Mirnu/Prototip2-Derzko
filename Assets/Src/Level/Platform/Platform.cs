using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Platform : StateObject
{
    [Header("Список перемещений платформы")] public List<MoveData> PlatformMovements;

    [Header("Двигается ли платформа сама")][SerializeField] private bool isAutomatic = false;

    [SerializeField] private float iterate = 0;

    private Vector3 _currrentStartPosition;
    private IEnumerator currentEnumerator;

    public override Dictionary<string, object> State { get; protected set; } = new Dictionary<string, object>()
    {
        {"isActive", false}
    };

    private void Awake()
    {
        _currrentStartPosition = transform.position;
        Subscribe("isActive", (active, prev) =>
        {
            Debug.Log(active);
            if ((bool)active) Move();
            else StopMoving();
        });
    }

    private void Start() {
        if (isAutomatic) 
            Move();
    }

    public void Move() {
        currentEnumerator = IterateOverMovements();
        if (PlatformMovements.IsEmpty()) return;
        StartCoroutine(currentEnumerator);
    }

    public void StopMoving() => StopCoroutine(currentEnumerator);

    public IEnumerator IterateOverMovements() {
        if (PlatformMovements.IsEmpty()) yield break;

        MoveData data = PlatformMovements[0];
            
        for (; iterate < data.moveTime; iterate += Time.deltaTime)
        {
            MovePlatformMove(data);
            yield return new WaitForEndOfFrame();
        }
        iterate = 0;
        _currrentStartPosition = transform.position;

        if (!data.loop)
            PlatformMovements.RemoveAt(0);

        currentEnumerator = IterateOverMovements();
        StartCoroutine(currentEnumerator);
    }

    private void MovePlatformMove(MoveData data) {
        Vector3 target = _currrentStartPosition + data.PositionChange;
        transform.position = Vector3.Lerp(transform.position, target, iterate / (data.moveTime * 100));
            //Костыль чтобы еррор не вылезал
        if(data.NewRotation != Quaternion.identity) 
             transform.rotation = Quaternion.Lerp(transform.rotation, data.NewRotation, iterate / (data.moveTime * 100));   
    }
    private void OnDrawGizmos() {
        if (PlatformMovements.IsEmpty()) return;
        Vector3 sum = Vector3.zero;
        foreach (var data in PlatformMovements)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + sum, transform.position + sum + data.PositionChange);
            sum += data.PositionChange;
            if (data.loop) return;
        }
    }
}