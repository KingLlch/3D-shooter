using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator Damage;
    private TakeDamage _takeDamage;
    private void Awake()
    {
        _takeDamage = GameObject.FindObjectOfType<TakeDamage>();

        _takeDamage.HitEnemyEvent.AddListener(DamageAnimation);
    }

    private void DamageAnimation()
    {
        Damage.Play("DamageUI");
    }
}
