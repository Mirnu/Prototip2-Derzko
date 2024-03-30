using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 50f;
    public float Damage = 100f;
    public float LifeTime = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseCharacter character))
        {
            character.Humanoid.TakeDamage(Damage);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    private void Start() => StartCoroutine(Disappear());

    private void Update()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime);
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(LifeTime);
        if (gameObject != null) 
            Destroy(gameObject);
    }
}
