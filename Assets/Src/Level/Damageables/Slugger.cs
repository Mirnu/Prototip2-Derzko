using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slugger : MonoBehaviour
{
    [SerializeField] private GameObject _muzzle;

    [SerializeField] private Bullet _bullet;

    [SerializeField] private float _waitTimeBetweenDamage = 1f;

    private void Start() => StartCoroutine(startShoot());

    private void OnDestroy() => StopAllCoroutines();

    private IEnumerator startShoot()
    {
        while (true)
        {
            Instantiate(_bullet, _muzzle.transform.position, _muzzle.transform.rotation);
            yield return new WaitForSeconds(_waitTimeBetweenDamage);
        }
    }
}