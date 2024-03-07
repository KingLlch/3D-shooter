using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PickUpController : MonoBehaviour
{
    private PlayerController _playerController;
    private RayCastManager _rayCastManager;
    private WeaponManager _weaponManager;

    private GameObject _item, _weapon;
    private bool _isPickUpItem;
    [HideInInspector] public bool _isPickUpWeapon;

    [HideInInspector] public UnityEvent PickUpWeapon;
    [HideInInspector] public UnityEvent PickOffWeapon;

    private void Awake()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();

        _playerController.DropItemButtonDown.AddListener(DropItem);
        _playerController.DropWeaponButtonDown.AddListener(DropWeapon);
        _playerController.PickWeaponOrItemButtonDown.AddListener(PickWeaponOrItem);
    }

    private void DropItem()
    {
        if (_isPickUpItem == true)
        {
            _item.GetComponent<Item>().PickOff();
            _isPickUpItem = false;
        }
    }

    private void DropWeapon()
    {
        if (_isPickUpWeapon == true)
        {
            _weapon.GetComponent<Weapon>().PickOff();
            _isPickUpWeapon = false;

            _weapon.GetComponent<RotateObject>().enabled = true;

            _weapon.GetComponent<Weapon>()._currentPatrons = _weaponManager._currentPatrons;
            _weapon.GetComponent<Weapon>()._maxPatrons = _weaponManager._maxPatrons;

            PickOffWeapon.Invoke();
        }
    }

    private void PickWeaponOrItem()
    {
        if (_isPickUpItem == false)
        {
            if (_rayCastManager._rayCastHit.collider.gameObject.GetComponent<Item>() && _rayCastManager.Distance <= 2f)
            {
                _rayCastManager._rayCastHit.collider.gameObject.GetComponent<Item>().PickUp();
                _item = _rayCastManager._rayCastHit.collider.gameObject;
                _isPickUpItem = true;
            }
        }

        if(_isPickUpWeapon == false)
        {
            if (_rayCastManager._rayCastHit.collider.gameObject.GetComponent<Weapon>() && _rayCastManager.Distance <= 2f)
            {
                _rayCastManager._rayCastHit.collider.gameObject.GetComponent<Weapon>().PickUp();
                _weapon = _rayCastManager._rayCastHit.collider.gameObject;
                _isPickUpWeapon = true;

                _weapon.GetComponent<RotateObject>().enabled = false;
                _weaponManager._damage = _weapon.GetComponent<Weapon>()._damage;
                _weaponManager._currentPatrons = _weapon.GetComponent<Weapon>()._currentPatrons;
                _weaponManager._maxPatrons = _weapon.GetComponent<Weapon>()._maxPatrons;

                PickUpWeapon.Invoke();
            }
        }
    }
}
