using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid
{
    public float MaxHealth { get; private set; } = 100;
    public float Health { get; private set; } = 100;

    public Humanoid()
    {
        Health = MaxHealth;
    }

    public Action<float> HealthChanged;
    public Action Died;

    public void TakeDamage(float damage)
    {
        if (Health - damage <= 0)
            Died?.Invoke();

        Health = Health - damage <= 0 ? MaxHealth : Health - damage;
        HealthChanged?.Invoke(Health);
    }
}
