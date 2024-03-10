using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TakeDamage : MonoBehaviour
{
    private RayCastManager _rayCastManager;
    private WeaponManager _weaponManager;
    private PlayerController _playerController;
    private PlayerHealth _playerHealth;

    private Camera _camera;

    private TextMeshProUGUI _damage;

    public UnityEvent ChangePlayerHealth;
    public UnityEvent ChangeEnemyHealth;

    private void Awake()
    {
        _camera = Camera.main;
        _damage = GameObject.Find("Damage").GetComponent<TextMeshProUGUI>();

        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        _weaponManager.ShotWithPatrons.AddListener(HitEnemy);
        _playerController.CollisionWithEnemy.AddListener(EnemyHit);
        _playerController.CollisionWithMedpack.AddListener(PickUpMedpack);
    }

    private void PickUpMedpack()
    {
        _playerHealth.GetHealth(_playerController.Medpack.GetComponent<Medpack>().Heal);
        Destroy(_playerController.Medpack);
        ChangePlayerHealth.Invoke();
    }

    private void EnemyHit()
    {
        _playerHealth.TakeDamage(_rayCastManager._rayCastHit.collider.GetComponent<Enemy>().Damage);
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
