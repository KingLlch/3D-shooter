using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthAndAmmo : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float Health { get; private set; } = 100;

    [field: SerializeField] public int[] Ammo { get; private set; } = new int[2];

    [HideInInspector] public UnityEvent GameOver;

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            GameOver.Invoke();
        }
    }

    public void GetHealth(float heal)
    {
        Health += heal;

        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void GetAmmo(int typeAmmo, int ammo)
    {
        Ammo[typeAmmo] += ammo;
    }
}
