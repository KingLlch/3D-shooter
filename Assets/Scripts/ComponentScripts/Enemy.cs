using UnityEngine;


public class Enemy : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float Health { get; private set; } = 100;
    [field: SerializeField] public float Damage { get; set; } = 10;
    [field: SerializeField] public bool IsPlayerSeek { get; private set; }

    [SerializeField] private float _enemySpeed = 1;
    [SerializeField] private float _playerSeekRange;

    [SerializeField] private GameObject _player;

    private void Awake()
    {
        gameObject.tag = "Enemy";
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (IsPlayerSeek == false)
        {
            if ((gameObject.transform.position - _player.transform.position).magnitude <= _playerSeekRange)
            {
                IsPlayerSeek = true;
            }
        }

        if (IsPlayerSeek == true)
        {
            SeekPlayer();
        }
    }
    private void SeekPlayer()
    {
        gameObject.transform.LookAt(_player.transform);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _player.transform.position, _enemySpeed * Time.fixedDeltaTime);

    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if ( Health <= 0)
        {
           Destroy(gameObject);
        } 
    }

    public void PlayerSeek(bool seek)
    {
        IsPlayerSeek = seek;
    }
}
