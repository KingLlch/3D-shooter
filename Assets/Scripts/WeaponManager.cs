using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private PlayerController _playerController;
    private SoundManager _soundManager;

    public int _currentBullets = 0;
    public int _maxBullets = 30;
    public int _bullets = 200;


    private void Awake()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _soundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public void shot()
    {
        if (_currentBullets > 0)
        {
            _currentBullets--;
            _soundManager.PistolShot();
        } 
    }

    public void reload()
    {
        if (( _bullets - _maxBullets - _currentBullets) >= 0)
        {
            _bullets -= (_maxBullets - _currentBullets);     
            _currentBullets = _maxBullets;
        }
            
        else
        {
            _bullets -= _maxBullets - _currentBullets;
            _currentBullets = _maxBullets + _bullets;
            _bullets = 0;
        }   
    }
}
