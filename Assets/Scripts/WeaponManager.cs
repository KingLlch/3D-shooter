using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PickUpController _pickUpController;

    public float _damage ;
    public int _currentPatrons = 0;
    public int _maxPatrons = 30;
    public int _patrons = 200;

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
                _currentPatrons--;
                ShotWithPatrons.Invoke();
            }

            else ShotWithoutPatrons.Invoke();

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
        }
    }
}
