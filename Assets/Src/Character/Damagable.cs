using System;
using UnityEngine;

public abstract class Damagable : MonoBehaviour {
    public float MaxHealth;
    public float CurrentHealth;
    public event Action OnKilled;
    public event Action OnDamaged;

    private void Start() {
        CurrentHealth = MaxHealth;
    }

    public virtual void damage(float amount) {
        if(CurrentHealth > amount) {
            OnDamaged?.Invoke();
            CurrentHealth -= amount;
        } else {
            OnKilled?.Invoke();
            CurrentHealth = 0;
        }
    }
}