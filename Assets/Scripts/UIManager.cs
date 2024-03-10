using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private PickUpController _pickUpController;
    private WeaponManager _weaponManager;
    private TakeDamage _takeDamage;
    private RayCastManager _rayCastManager;

    [SerializeField] private float timeHideEnemyHealth = 5;
    private float timerHideEnemyHealth;

    private GameObject _bulletsUI;
    private GameObject _enemyHealthUI;
    private TextMeshProUGUI _enemyHealth;

    private TextMeshProUGUI _bullets;

    private GameObject[] _changeTypeShotUI = new GameObject[2];

    private void Awake()
    {
        _bulletsUI = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI");
        _enemyHealthUI = GameObject.Find("UI/MainCanvas/EnemyHealthUI");
        _changeTypeShotUI[0] = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI/TypeShot/TypeShotImage");
        _changeTypeShotUI[1] = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI/TypeShot/TypeShotImage1");
        _enemyHealth = GameObject.Find("UI/MainCanvas/EnemyHealthUI/EnemyHealth").GetComponent<TextMeshProUGUI>();
        _bullets = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI/Bullets").GetComponent<TextMeshProUGUI>();

        _pickUpController = GameObject.FindObjectOfType<PickUpController>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _takeDamage = GameObject.FindObjectOfType<TakeDamage>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();

        _pickUpController.PickUpWeapon.AddListener(PickWeapon);
        _pickUpController.PickOffWeapon.AddListener(DropWeapon);
        _weaponManager.ShotWithPatrons.AddListener(ChangeValueBullets);
        _weaponManager.ReloadEvent.AddListener(ChangeValueBullets);
        _weaponManager.ChangeTypeShotEvent.AddListener(ChangeTypeShot);
        _takeDamage.HitEnemyEvent.AddListener(ShowEnemyHealth);

        _bulletsUI.SetActive(false);
        _enemyHealthUI.SetActive(false);
        _changeTypeShotUI[0].SetActive(false);
    }
    private void FixedUpdate()
    {
        timerHideEnemyHealth += Time.fixedDeltaTime;
        if (timerHideEnemyHealth > timeHideEnemyHealth)
        {
            _enemyHealthUI.SetActive(false);
            timerHideEnemyHealth = 0;
        }
    }
    private void Start()
    {
        ChangeValueBullets();
    }

    private void PickWeapon()
    {
        _bulletsUI.SetActive(true);
        ChangeValueBullets();
    }

    private void DropWeapon()
    {
        _bulletsUI.SetActive(false);
    }

    private void ChangeValueBullets()
    {
        _bullets.text = _weaponManager._currentPatrons.ToString() + "/" + _weaponManager._patrons.ToString();
    }

    private void ShowEnemyHealth()
    {
        if (_rayCastManager._rayCastHit.collider != null)
        {
            _enemyHealth.text = _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().Health.ToString();
            _enemyHealthUI.SetActive(true);
            timerHideEnemyHealth = 0;
        }
    }

    private void ChangeTypeShot()
    {
        if (_changeTypeShotUI[1].activeInHierarchy == true)
        {
            _changeTypeShotUI[1].SetActive(false);
            _changeTypeShotUI[0].SetActive(true);
        }
        else 
        {
            _changeTypeShotUI[0].SetActive(false);
            _changeTypeShotUI[1].SetActive(true);
        } 
    }

}
