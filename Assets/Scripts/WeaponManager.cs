using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PickUpController _pickUpController;

    public float _damage;
    public int _currentPatrons = 0;
    public int _maxPatrons = 30;
    public int _patrons = 200;
    public float _timeReload;
    public float _timeShot;

    private float _timerShot;
    private float _timerDryShot;

    private bool _isShot;

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
            if (_currentPatrons > 0)
            {
                _timerShot += Time.fixedDeltaTime;
                if (_timeShot <= _timerShot) _isShot = true;
                else _isShot = false;

                if (_isShot == false)
                {
                    return;
                }
                _currentPatrons--;
                ShotWithPatrons.Invoke();

                _timerShot = 0;
            }

            else
            {
                _timerDryShot += Time.fixedDeltaTime;
                if (_timerDryShot >= 2)
                {
                    ShotWithoutPatrons.Invoke();
                    _timerDryShot = 0;
                }
            }
        }
    }

    private void reload()
    {
        if (_pickUpController._isPickUpWeapon == true)
        {
            if ((_patrons - _maxPatrons + _currentPatrons) >= 0)
            {
                _patrons -= (_maxPatrons - _currentPatrons);
                _currentPatrons = _maxPatrons;
            }

            else
            {
                _currentPatrons += _patrons;
                _patrons = 0;
            }

            Reload.Invoke();
            _timerShot = - _timeReload;
        }
    }
}
