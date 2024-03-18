using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PickUpController _pickUpController;
    private WeaponManager _weaponManager;
    private CollideManager _collideManager;
    private RayCastManager _rayCastManager;
    private PlayerHealthAndAmmo _playerHealthAndAmmo;
    private PlayerController _playerController;

    private float timeHideEnemyHealth = 5;
    private float timerHideEnemyHealth;

    private GameObject _ammoUI;
    private GameObject _enemyHealthUI;
    private GameObject[] _changeTypeShotUI = new GameObject[2];
    private GameObject _pauseUI;
    private GameObject _gameOverUI;

    private TextMeshProUGUI _enemyHealthTMPro;
    private TextMeshProUGUI _ammoTMPro;
    private TextMeshProUGUI _playerHealthTMPro;

    private Image[] _EnemyHealthtImage = new Image[2];
    private Image _playerHealthImage;

    private void Awake()
    {
        _ammoUI = GameObject.Find("UI/MainCanvas/WeaponUI/AmmoUI");
        _enemyHealthUI = GameObject.Find("UI/MainCanvas/EnemyHealthUI");
        _changeTypeShotUI[0] = GameObject.Find("UI/MainCanvas/WeaponUI/AmmoUI/TypeShot/TypeShotImage");
        _changeTypeShotUI[1] = GameObject.Find("UI/MainCanvas/WeaponUI/AmmoUI/TypeShot/TypeShotImage1");
        _pauseUI = GameObject.Find("UI/MainCanvas/Pause");
        _gameOverUI = GameObject.Find("UI/MainCanvas/GameOver");

        _playerHealthImage = GameObject.Find("UI/MainCanvas/HealthUI/HealthBar").GetComponent<Image>();
        _EnemyHealthtImage[0] = GameObject.Find("UI/MainCanvas/EnemyHealthUI/HealthBar").GetComponent<Image>();
        _EnemyHealthtImage[1] = GameObject.Find("UI/MainCanvas/EnemyHealthUI/HealthBar1").GetComponent<Image>();

        _enemyHealthTMPro = GameObject.Find("UI/MainCanvas/EnemyHealthUI/EnemyHealth").GetComponent<TextMeshProUGUI>();
        _playerHealthTMPro = GameObject.Find("UI/MainCanvas/HealthUI/Health").GetComponent<TextMeshProUGUI>();
        _ammoTMPro = GameObject.Find("UI/MainCanvas/WeaponUI/AmmoUI/Ammo").GetComponent<TextMeshProUGUI>();

        _pickUpController = GameObject.FindObjectOfType<PickUpController>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        _collideManager = GameObject.FindObjectOfType<CollideManager>();
        _rayCastManager = GameObject.FindObjectOfType<RayCastManager>();
        _playerHealthAndAmmo = GameObject.FindObjectOfType<PlayerHealthAndAmmo>();
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        _pickUpController.PickUpWeapon.AddListener(PickWeapon);
        _pickUpController.PickOffWeapon.AddListener(DropWeapon);
        _pickUpController.PickOffWeapon.AddListener(ShowTypeShot);
        _weaponManager.ShotWithPatrons.AddListener(ChangeValueAmmo);
        _weaponManager.ReloadEvent.AddListener(ChangeValueAmmo);
        _weaponManager.ChangeTypeShotEvent.AddListener(ShowTypeShot);
        _collideManager.ChangePlayerHealth.AddListener(ShowPlayerHealth);
        _collideManager.ChangeValueAmmo.AddListener(ChangeValueAmmo);
        _collideManager.ChangeEnemyHealth.AddListener(ShowEnemyHealth);
        _playerHealthAndAmmo.GameOver.AddListener(GameOver);
        _playerController.EscapeButtonDown.AddListener(Pause);

        _ammoUI.SetActive(false);
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
        ChangeValueAmmo();
    }

    private void PickWeapon()
    {
        _ammoUI.SetActive(true);
        ChangeValueAmmo();
    }

    private void DropWeapon()
    {
        _ammoUI.SetActive(false);
    }

    private void ChangeValueAmmo()
    {
        _ammoTMPro.text = _weaponManager.CurrentAmmo.ToString() + "/" + _playerHealthAndAmmo.Ammo[_weaponManager._typeAmmo].ToString();
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

    private void ShowTypeShot()
    {
        if (_weaponManager._isSingleShoot)
        {
            _changeTypeShotUI[0].SetActive(false);
            _changeTypeShotUI[1].SetActive(true);
        }
        else
        {
            _changeTypeShotUI[1].SetActive(false);
            _changeTypeShotUI[0].SetActive(true);
        }
    }

    private void ShowPlayerHealth()
    {
        _playerHealthTMPro.text = _playerHealthAndAmmo.Health.ToString();
        _playerHealthImage.fillAmount = _playerHealthAndAmmo.Health / _playerHealthAndAmmo.MaxHealth;
    }

    private void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        _gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        if (_playerController.IsPaused)
        {
            Resume();
            return;
        }

        Time.timeScale = 0;

        _playerController.IsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        _pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;

        _playerController.IsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _pauseUI.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
