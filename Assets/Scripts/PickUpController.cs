using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PickUpController : MonoBehaviour
{
    private PlayerController _playerController;
    private RayCastManager _rayCastManager;

    private GameObject _item, _weapon;
    private bool _isPickUpItem;
    [HideInInspector] public bool _isPickUpWeapon;

    [HideInInspector] public UnityEvent PickUpWeapon;
    [HideInInspector] public UnityEvent PickOffWeapon;

    private void Awake()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();

        _playerController.DropItemButtonDown.AddListener(DropItem);
        _playerController.DropWeaponButtonDown.AddListener(DropWeapon);
        _playerController.PickWeaponOrItemButtonDown.AddListener(PickWeaponOrItem);
    }

    private void DropItem()
    {
        if (_isPickUpItem == true)
        {
            _item.GetComponent<PickUpItem>().PickOff();
            _isPickUpItem = false;
        }
    }

    private void DropWeapon()
    {
        if (_isPickUpWeapon == true)
        {
            _weapon.GetComponent<PickUpWeapon>().PickOff();
            _isPickUpWeapon = false;

            PickOffWeapon.Invoke();
        }
    }

    private void PickWeaponOrItem()
    {
        if ((_isPickUpItem == false) || (_isPickUpWeapon == false))
        {
            if (_rayCastManager._rayCastHit.collider.gameObject.GetComponent<PickUpItem>() && _rayCastManager.Distance <= 2f)
            {
                _rayCastManager._rayCastHit.collider.gameObject.GetComponent<PickUpItem>().PickUp();
                _item = _rayCastManager._rayCastHit.collider.gameObject;
                _isPickUpItem = true;
            }

            if (_rayCastManager._rayCastHit.collider.gameObject.GetComponent<PickUpWeapon>() && _rayCastManager.Distance <= 2f)
            {
                _rayCastManager._rayCastHit.collider.gameObject.GetComponent<PickUpWeapon>().PickUp();
                _weapon = _rayCastManager._rayCastHit.collider.gameObject;
                _isPickUpWeapon = true;

                PickUpWeapon.Invoke();
            }
        }
    }
}

