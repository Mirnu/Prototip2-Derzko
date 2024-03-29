using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Dummy : Damagable {

    private void Awake() {
        OnDamaged += delegate { GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); };
        OnKilled += delegate {GetComponent<SpriteRenderer>().color = Color.black;};
    }
}