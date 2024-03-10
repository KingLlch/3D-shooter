using UnityEngine;


public class Enemy : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float Health { get; private set; } = 100;

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if ( Health <= 0)
        {
           Destroy(gameObject);
        } 
    }
}
