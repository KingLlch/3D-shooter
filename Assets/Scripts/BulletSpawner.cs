using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private RayCastManager _rayCastManager;
    private WeaponManager _weaponManager;

    [SerializeField] private GameObject _gameObject;

    private void Awake()
    {
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();

        _weaponManager.ShotWithPatrons.AddListener(Shot);
    }

    private void Shot()
    {
        if (_rayCastManager._rayCastHit.collider != null)
        Instantiate(_gameObject, _rayCastManager._rayCastHit.point,Quaternion.identity,_rayCastManager._rayCastHit.collider.transform);
    }
}
