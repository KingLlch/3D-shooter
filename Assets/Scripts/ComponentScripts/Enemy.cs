using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _health = 100;

    public float Health 
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }     
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if ( _health <= 0)
        {
           Destroy(gameObject);
        } 
    }
}
