using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator Damage;
    private CollideManager _collideManager;
    private void Awake()
    {
        _collideManager = GameObject.FindObjectOfType<CollideManager>();

        _collideManager.ChangeEnemyHealth.AddListener(DamageAnimation);
    }

    private void DamageAnimation()
    {
        Damage.Play("DamageUI");
    }
}
