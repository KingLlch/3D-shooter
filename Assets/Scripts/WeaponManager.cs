using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PickUpController _pickUpController;

    public int _currentBullets = 0;
    public int _maxBullets = 30;
    public int _bullets = 200;

    [HideInInspector] public UnityEvent ShotWithPatrons;
    [HideInInspector] public UnityEvent ShotWithoutPatrons;
    [HideInInspector] public UnityEvent Reload;

    private void Awake()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _pickUpController = GameObject.FindObjectOfType<PickUpController>();

        _playerController.ShotButtonDown.AddListener(Shot);
        _playerController.ReloadButtonDown.AddListener(reload);
    }

    private void Shot()
    {
        if (_pickUpController._isPickUpWeapon == true)
        {
            if (_currentBullets > 0)
            {
                _currentBullets--;
                ShotWithPatrons.Invoke();
            }

            else ShotWithoutPatrons.Invoke();

        }
    }

    private void reload()
    {
        if (_pickUpController._isPickUpWeapon == true)
        {
            if (_bullets <= 0) return;

            if ((_bullets - _maxBullets - _currentBullets) >= 0)
            {
                _bullets -= (_maxBullets - _currentBullets);
                _currentBullets = _maxBullets;
            }

            else
            {
                _currentBullets += _bullets;
                _bullets -= _maxBullets - _currentBullets;

                if (_bullets < 0)
                {
                    _bullets = 0;
                    return;
                }

                _currentBullets = _maxBullets;
            }

            Reload.Invoke();
        }
    }
}
