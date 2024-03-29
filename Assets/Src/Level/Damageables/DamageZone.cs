using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageZone : MonoBehaviour {
    [SerializeField] private float DamageAmount = 10f;
    [SerializeField] private float WaitTimeBetweenDamage = 10f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.TryGetComponent(out BaseCharacter d)) return;
        Debug.Log("ender: " + d);
        StartCoroutine(applyDamage(d.Humanoid));
    }

    private void OnTriggerExit2D(Collider2D other) {
        StopAllCoroutines();
    }

    IEnumerator applyDamage(Humanoid humanoid) {
        while (true)
        {
            humanoid.TakeDamage(DamageAmount);
            yield return new WaitForSeconds(WaitTimeBetweenDamage);
        }
    }
}