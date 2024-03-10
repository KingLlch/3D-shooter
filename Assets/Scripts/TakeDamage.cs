using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TakeDamage : MonoBehaviour
{
    private RayCastManager _rayCastManager;
    private WeaponManager _weaponManager;

    private Camera _camera;

    private TextMeshProUGUI _damage;
    public UnityEvent HitEnemyEvent;

    private void Awake()
    {
        _camera = Camera.main;
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
            _damage.text = _weaponManager._damage.ToString();
            HitEnemyEvent.Invoke();
        }
    }
}
