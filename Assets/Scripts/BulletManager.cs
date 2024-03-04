using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private RayCastController _rayCastController;
    private PlayerController _playerController;
    private WeaponManager _weaponManager;

    [SerializeField] private GameObject _gameObject;


    private void Awake()
    {
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerController.Shot.AddListener(shot);
        _rayCastController = GameObject.FindObjectOfType<RayCastController>();
    }

    private void shot()
    {
        if(_weaponManager._currentBullets>0)
        Instantiate(_gameObject, _rayCastController._rayCastHit.point,Quaternion.identity);
    }
}
