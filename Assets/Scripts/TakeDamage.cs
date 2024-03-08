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

    private TextMeshProUGUI _damage;
    private TextMeshProUGUI _enemyHealth;

    public UnityEvent Damage;

    private void Awake()
    {
        _camera = Camera.main;
        _damage = GameObject.Find("Damage").GetComponent<TextMeshProUGUI>();
        _enemyHealth = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();

        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();

        _weaponManager.ShotWithPatrons.AddListener(HitEnemy);
        Damage.AddListener(ShowEnemyHealthPanel);
    }

    private void HitEnemy()
    {
        if ((_rayCastManager._rayCastHit.collider != null) &&(_rayCastManager._rayCastHit.collider.GetComponent<Enemy>()))
        {    
            _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().TakeDamage(_weaponManager._damage);
            _damage.transform.position = _camera.WorldToScreenPoint(_rayCastManager._rayCastHit.point);
            _damage.text = _weaponManager._damage.ToString();
            Damage.Invoke();
        }
    }

    private void ShowEnemyHealthPanel()
    {
        if (_rayCastManager._rayCastHit.collider != null)
        _enemyHealth.text = _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().Health.ToString();
    }
}
