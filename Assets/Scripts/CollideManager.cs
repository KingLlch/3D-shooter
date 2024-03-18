using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CollideManager : MonoBehaviour
{
    private RayCastManager _rayCastManager;
    private WeaponManager _weaponManager;
    private PlayerController _playerController;
    private PlayerHealthAndAmmo _playerHealthAndAmmo;

    private Camera _camera;

    private TextMeshProUGUI _damage;

    [HideInInspector] public UnityEvent ChangePlayerHealth;
    [HideInInspector] public UnityEvent ChangePlayerAmmo;
    [HideInInspector] public UnityEvent ChangeEnemyHealth;
    [HideInInspector] public UnityEvent ChangeValueAmmo;

    private void Awake()
    {
        _camera = Camera.main;
        _damage = GameObject.Find("Damage").GetComponent<TextMeshProUGUI>();

        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _playerHealthAndAmmo = GameObject.FindObjectOfType<PlayerHealthAndAmmo>();

        _weaponManager.ShotWithPatrons.AddListener(HitEnemy);
        _playerController.CollisionWithEnemy.AddListener(EnemyHit);
        _playerController.CollisionWithMedpack.AddListener(PickUpMedpack);
        _playerController.CollisionWithAmmo.AddListener(PickUpAmmo);
    }

    private void PickUpAmmo()
    {
        _playerHealthAndAmmo.GetAmmo(_playerController.Ammopack.GetComponent<Ammo>().TypeAmmo,_playerController.Ammopack.GetComponent<Ammo>().Ammunition);
        Destroy(_playerController.Ammopack);
        ChangeValueAmmo.Invoke();
    }

    private void PickUpMedpack()
    {
        _playerHealthAndAmmo.GetHealth(_playerController.Medpack.GetComponent<Medpack>().Heal);
        Destroy(_playerController.Medpack);
        ChangePlayerHealth.Invoke();
    }

    private void EnemyHit()
    {
        _playerHealthAndAmmo.TakeDamage(_playerController.Enemy.GetComponent<Enemy>().Damage);
        ChangePlayerHealth.Invoke();
    }

    private void HitEnemy()
    {
        if ((_rayCastManager._rayCastHit.collider != null) &&(_rayCastManager._rayCastHit.collider.GetComponent<Enemy>()))
        {    
            _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().TakeDamage(_weaponManager._damage);
            _damage.transform.position = _camera.WorldToScreenPoint(_rayCastManager._rayCastHit.point);
            _damage.text = _weaponManager._damage.ToString();

            _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().PlayerSeek(true);
            ChangeEnemyHealth.Invoke();
        }
    }
}
