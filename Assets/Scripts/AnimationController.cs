using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator Damage;
    private TakeDamageManager _takeDamage;
    private void Awake()
    {
        _takeDamage = GameObject.FindObjectOfType<TakeDamageManager>();

        _takeDamage.ChangeEnemyHealth.AddListener(DamageAnimation);
    }

    private void DamageAnimation()
    {
        Damage.Play("DamageUI");
    }
}
