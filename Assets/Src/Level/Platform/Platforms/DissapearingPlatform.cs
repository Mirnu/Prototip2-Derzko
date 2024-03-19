using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : Platform
{
    [Header("Через сколько секунд под игроком исчезает платфома")][SerializeField] private float _maxPlayerCollisionTime = 2f;
    [Header("Время восстановления")][SerializeField] private float _regenerationTime = 1f;
    private const string playerLayer = "Player";
    private bool hasMoved = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.gameObject.layer != LayerMask.NameToLayer(playerLayer) || hasMoved) return;
        StartCoroutine(SetActiveAfterTime(_maxPlayerCollisionTime, false));
        Move();
        hasMoved = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.collider.gameObject.layer != LayerMask.NameToLayer(playerLayer)) return;
        StartCoroutine(SetActiveAfterTime(_regenerationTime, true));
    }

    IEnumerator SetActiveAfterTime(float time, bool active) {
        yield return new WaitForSeconds(time);
        //temp
        State["isActive"] = active;
        gameObject.GetComponent<Collider2D>().enabled = active;
        gameObject.GetComponent<SpriteRenderer>().enabled = active;
    }
}
