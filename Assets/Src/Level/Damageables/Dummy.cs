using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Dummy : BaseCharacter {

    private void Awake() {
        Humanoid.HealthChanged += delegate { GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); };
        Humanoid.Died += delegate {GetComponent<SpriteRenderer>().color = Color.black;};
    }
}