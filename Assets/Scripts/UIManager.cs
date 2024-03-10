using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PickUpController _pickUpController;
    private WeaponManager _weaponManager;
    private TakeDamageManager _takeDamage;
    private RayCastManager _rayCastManager;
    private PlayerHealth _playerHealth;

    private float timeHideEnemyHealth = 5;
    private float timerHideEnemyHealth;

    private GameObject _bulletsUI;
    private GameObject _enemyHealthUI;
    private GameObject[] _changeTypeShotUI = new GameObject[2];

    private TextMeshProUGUI _enemyHealthTMPro;
    private TextMeshProUGUI _bulletsTMPro;
    private TextMeshProUGUI _playerHealthTMPro;

    private Image[] _EnemyHealthtImage = new Image[2];
    private Image _playerHealthImage;

    private void Awake()
    {
        _bulletsUI = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI");
        _enemyHealthUI = GameObject.Find("UI/MainCanvas/EnemyHealthUI");
        _changeTypeShotUI[0] = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI/TypeShot/TypeShotImage");
        _changeTypeShotUI[1] = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI/TypeShot/TypeShotImage1");

        _playerHealthImage = GameObject.Find("UI/MainCanvas/HealthUI/HealthBar").GetComponent<Image>();
        _EnemyHealthtImage[0] = GameObject.Find("UI/MainCanvas/EnemyHealthUI/HealthBar").GetComponent<Image>();
        _EnemyHealthtImage[1] = GameObject.Find("UI/MainCanvas/EnemyHealthUI/HealthBar1").GetComponent<Image>();

        _enemyHealthTMPro = GameObject.Find("UI/MainCanvas/EnemyHealthUI/EnemyHealth").GetComponent<TextMeshProUGUI>();
        _playerHealthTMPro = GameObject.Find("UI/MainCanvas/HealthUI/Health").GetComponent<TextMeshProUGUI>();
        _bulletsTMPro = GameObject.Find("UI/MainCanvas/WeaponUI/BulletsUI/Bullets").GetComponent<TextMeshProUGUI>();

        _pickUpController = GameObject.FindObjectOfType<PickUpController>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _takeDamage = GameObject.FindObjectOfType<TakeDamageManager>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        _pickUpController.PickUpWeapon.AddListener(PickWeapon);
        _pickUpController.PickOffWeapon.AddListener(DropWeapon);
        _weaponManager.ShotWithPatrons.AddListener(ChangeValueBullets);
        _weaponManager.ReloadEvent.AddListener(ChangeValueBullets);
        _weaponManager.ChangeTypeShotEvent.AddListener(ChangeTypeShot);
        _takeDamage.ChangePlayerHealth.AddListener(ShowPlayerHealth);
        _takeDamage.ChangeEnemyHealth.AddListener(ShowEnemyHealth);
        _playerHealth.GameOver.AddListener(GameOver);

        _bulletsUI.SetActive(false);
        _enemyHealthUI.SetActive(false);
        _changeTypeShotUI[0].SetActive(false);

        ShowPlayerHealth();
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
        _bulletsTMPro.text = _weaponManager._currentPatrons.ToString() + "/" + _weaponManager._patrons.ToString();
    }

    private void ShowEnemyHealth()
    {
        if (_rayCastManager._rayCastHit.collider != null)
        {
            _enemyHealthTMPro.text = _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().Health.ToString();
            _enemyHealthUI.SetActive(true);

            _EnemyHealthtImage[0].fillAmount = _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().Health / _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().MaxHealth;
            _EnemyHealthtImage[1].fillAmount = _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().Health / _rayCastManager._rayCastHit.collider.GetComponent<Enemy>().MaxHealth;

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
    private void ShowPlayerHealth()
    {
        _playerHealthTMPro.text = _playerHealth.Health.ToString();
        _playerHealthImage.fillAmount = _playerHealth.Health / _playerHealth.MaxHealth;
    }
    private void GameOver()
    {
        Debug.Log("GameOver");
    }

}
