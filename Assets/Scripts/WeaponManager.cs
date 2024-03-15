using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PickUpController _pickUpController;
    private PlayerHealthAndAmmo _playerHealthAndAmmo;

    private float _timerShot; 
    private float _timerReload;
    private float _timerDryShot;

    private bool _isShot;
    public bool _isSingleShoot = true;

    public int CurrentAmmo { get; private set; } = 0;
    public int MaxAmmo { get; private set; } = 30;

    [HideInInspector] public int _typeAmmo;

    [HideInInspector] public float _damage;
    [HideInInspector] public float _timeReload;
    [HideInInspector] public float _timeShot;
    [HideInInspector] public float _recoil = 0.3f;

    [HideInInspector] public UnityEvent ShotWithPatrons;
    [HideInInspector] public UnityEvent ShotWithoutPatrons;
    [HideInInspector] public UnityEvent ReloadEvent;
    [HideInInspector] public UnityEvent ChangeTypeShotEvent;

    private void Awake()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _pickUpController = GameObject.FindObjectOfType<PickUpController>();
        _playerHealthAndAmmo = GameObject.FindObjectOfType<PlayerHealthAndAmmo>();

        _playerController.ShotButtonDownSingle.AddListener(Shot);
        _playerController.ReloadButtonDown.AddListener(Reload);
        _playerController.ChangeTypeShot.AddListener(ChangeTypeShot);
    }

    private void ChangeTypeShot()
    {
        _isSingleShoot = !_isSingleShoot;

        if (_isSingleShoot)
        {
            _playerController.ShotButtonDownMulti.RemoveListener(Shot);
            _playerController.ShotButtonDownSingle.AddListener(Shot);

        }

        else
        {
            _playerController.ShotButtonDownSingle.RemoveListener(Shot);
            _playerController.ShotButtonDownMulti.AddListener(Shot);
        }

        ChangeTypeShotEvent.Invoke();
    }

    private void Shot()
    {
        if (_pickUpController._isPickUpWeapon == true)
        {
            if (CurrentAmmo > 0)
            {
                if (_isSingleShoot == true)
                {
                    Recoil();

                    CurrentAmmo--;
                    ShotWithPatrons.Invoke();
                    return;
                }
                _timerReload += Time.fixedDeltaTime;
                if (_timerReload <= 0) return;
                _timerShot += Time.fixedDeltaTime;
                if (_timerShot >= _timeShot) _isShot = true;
                else _isShot = false;

                if (_isShot == false)
                {
                    return;
                }
                _timerShot = 0;

                Recoil();

                CurrentAmmo--;
                ShotWithPatrons.Invoke();
            }

            else
            {
                if (_isSingleShoot == true)
                {
                    ShotWithoutPatrons.Invoke();
                    return;
                }

                _timerDryShot += Time.fixedDeltaTime;
                if (_timerDryShot >= 2)
                {
                    ShotWithoutPatrons.Invoke();
                    _timerDryShot = 0;
                }
            }
            
        }
    }

    private void Recoil()
    {
        _playerController.Head.transform.Rotate(-_recoil - Random.Range(0.1f,0.6f), 0, 0);
    }

    private void Reload()
    {
        if (_pickUpController._isPickUpWeapon == true)
        {
            if ((_playerHealthAndAmmo.Ammo[_typeAmmo] - MaxAmmo + CurrentAmmo) >= 0)
            {
                _playerHealthAndAmmo.Ammo[_typeAmmo] -= (MaxAmmo - CurrentAmmo);
                CurrentAmmo = MaxAmmo;
            }

            else
            {
                CurrentAmmo += _playerHealthAndAmmo.Ammo[_typeAmmo];
                _playerHealthAndAmmo.Ammo[_typeAmmo] = 0;
            }

            ReloadEvent.Invoke();
            _timerReload = - _timeReload;
        }
    }
    public void SetCurrentAmmo(int curentAmmo)
    {
        CurrentAmmo = curentAmmo;
    }

    public void SetMaxAmmo(int maxAmmo)
    {
        MaxAmmo = maxAmmo;
    }

}
