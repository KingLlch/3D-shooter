using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private RayCastManager _rayCastManager;
    private PlayerController _playerController;
    private WeaponManager _weaponManager;

    [SerializeField] private GameObject _gameObject;


    private void Awake()
    {
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _weaponManager.ShotWithPatrons.AddListener(shot);
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();
    }

    private void shot()
    {
        Instantiate(_gameObject, _rayCastManager._rayCastHit.point,Quaternion.identity);
    }
}
