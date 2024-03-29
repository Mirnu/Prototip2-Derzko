using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageZone : MonoBehaviour {
    [SerializeField] private float DamageAmount = 10f;
    [SerializeField] private float WaitTimeBetweenDamage = 10f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.TryGetComponent(out Damagable d)) return;
        Debug.Log("ender: " + d);
        StartCoroutine(applyDamage(d));
    }

    private void OnTriggerExit2D(Collider2D other) {
        StopAllCoroutines();
    }

    IEnumerator applyDamage(Damagable damageable) {
        if(damageable != null) {
            damageable.damage(DamageAmount);
            yield return new WaitForSeconds(WaitTimeBetweenDamage);
            StartCoroutine(applyDamage(damageable));
        } else {
            yield return null;
        }
    }
}