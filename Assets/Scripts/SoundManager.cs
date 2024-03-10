using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private WeaponManager _weaponManager;
    private AudioSource _weaponSource;
    private AudioSource _playerSource;
    private TakeDamageManager _takeDamage;

    [SerializeField] private AudioClip _shotWithPatrons, _shotWithOutPatrons, _reload, _enemyDamage;

    private void Awake()
    {
        _playerSource = GameObject.Find("Head").GetComponent<AudioSource>();
        _weaponSource = GameObject.Find("WeaponHand").GetComponent<AudioSource>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _takeDamage = GameObject.FindObjectOfType<TakeDamageManager>();

        _weaponManager.ShotWithPatrons.AddListener(ShotWithPatrons);
        _weaponManager.ShotWithoutPatrons.AddListener(ShotWithoutPatrons);
        _weaponManager.ReloadEvent.AddListener(Reload);
        _takeDamage.ChangePlayerHealth.AddListener(HitOnPlayer);
    }

    private void HitOnPlayer()
    {
        _playerSource.clip = _enemyDamage;
        _playerSource.Play();
    }

    private void ShotWithPatrons()
    {
        _weaponSource.clip = _shotWithPatrons;
        _weaponSource.Play();
    }

    private void ShotWithoutPatrons()
    {
        _weaponSource.clip = _shotWithOutPatrons;
        _weaponSource.Play();
    }

    private void Reload()
    {
        _weaponSource.clip = _reload;
        _weaponSource.Play();
    }
}
