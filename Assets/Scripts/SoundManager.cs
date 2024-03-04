using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _pistolShot, _pistolReload;
    private AudioSource _weaponSource;

    private void Awake()
    {
        _weaponSource = GameObject.Find("WeaponHand").GetComponent<AudioSource>();
    }
    public void PistolShot()
    {
        _weaponSource.clip = _pistolShot;
        _weaponSource.Play();
    }
    public void PistolReload()
    {
        _weaponSource.clip = _pistolReload;
        _weaponSource.Play();
    }
}
