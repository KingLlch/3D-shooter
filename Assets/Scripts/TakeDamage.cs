using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TakeDamage : MonoBehaviour
{
    private RayCastManager _rayCastManager;
    private WeaponManager _weaponManager;

    private Camera _camera;
    private GameObject _damageUI;
    private TextMeshProUGUI _damage;

    public UnityEvent Damage;

    private void Awake()
    {
        _camera = Camera.main;
        _damageUI = GameObject.Find("DamageUI");
        _damage = GameObject.Find("Damage").GetComponent<TextMeshProUGUI>();

        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();

        _weaponManager.ShotWithPatrons.AddListener(HitEnemy);
    }

    private void HitEnemy()
    {
        if ((_rayCastManager._rayCastHit.collider != null) &&(_rayCastManager._rayCastHit.collider.GetComponent<Enemy>()))
        {    
            _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().TakeDamage(_weaponManager._damage);
            _damage.transform.position = _camera.WorldToScreenPoint(_rayCastManager._rayCastHit.point);
            //_damage.text = _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().Health.ToString();
            _damage.text = _weaponManager._damage.ToString();
            Damage.Invoke();
        }
    }
}
