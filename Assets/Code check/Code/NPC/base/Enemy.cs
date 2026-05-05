using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, IDamageable
{
    public float MaxHealth { get; set; } = 100f; /// need to be change to realistic shit 
    public float CurrentHealth { get; set; }

    public void Damage(float damageAmount)
    {
        CurrentHealth-=damageAmount;
        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
    

