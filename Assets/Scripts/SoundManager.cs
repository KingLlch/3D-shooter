using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private WeaponManager _weaponManager;
    private AudioSource _weaponSource;

    [SerializeField] private AudioClip _shotWithPatrons, _shotWithOutPatrons, _reload;

    private void Awake()
    {
        _weaponSource = GameObject.Find("WeaponHand").GetComponent<AudioSource>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();

        _weaponManager.ShotWithPatrons.AddListener(ShotWithPatrons);
        _weaponManager.ShotWithoutPatrons.AddListener(ShotWithoutPatrons);
        _weaponManager.Reload.AddListener(Reload);
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
