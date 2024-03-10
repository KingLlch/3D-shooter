using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    private PlayerController _playerController;
    private PickUpController _pickUpController;

    private float _timerShot; 
    private float _timerReload;
    private float _timerDryShot;

    private bool _isShot;
    public bool _isSingleShoot = true;

    [HideInInspector] public int _currentPatrons = 0;
    [HideInInspector] public int _maxPatrons = 30;
    [HideInInspector] public int _patrons = 200;
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

        _playerController.ShotButtonDownSingle.AddListener(Shot);
        _playerController.ReloadButtonDown.AddListener(Reload);
        _playerController.ChangeTypeShot.AddListener(ChangeTypeShot);
    }

    private void ChangeTypeShot()
    {
        _isSingleShoot = !_isSingleShoot;
        ChangeTypeShotEvent.Invoke();

        if (_isSingleShoot == true)
        {
            _playerController.ShotButtonDownMulti.RemoveListener(Shot);
            _playerController.ShotButtonDownSingle.AddListener(Shot);

        }

        else
        {
            _playerController.ShotButtonDownSingle.RemoveListener(Shot);
            _playerController.ShotButtonDownMulti.AddListener(Shot);
        }
    }

    private void Shot()
    {
        if (_pickUpController._isPickUpWeapon == true)
        {
            if (_currentPatrons > 0)
            {
                if (_isSingleShoot == true)
                {
                    Recoil();

                    _currentPatrons--;
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

                _currentPatrons--;
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

            ReloadEvent.Invoke();
            _timerReload = - _timeReload;
        }
    }
}
