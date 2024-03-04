using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    private GameObject _UIbullets;
    private TextMeshProUGUI _bullets;
    private WeaponManager _weaponManager;
    private PlayerController _playerController;

    private void Awake()
    {
        _UIbullets = GameObject.Find("BulletsUI");
        _bullets = GameObject.Find("Bullets").GetComponent<TextMeshProUGUI>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();

        _playerController.ChangeValueBullets.AddListener(changeValueBullets);
        _playerController.PickWeapon.AddListener(PickWeapon);
        _playerController.DropWeapon.AddListener(DropWeapon);
        _UIbullets.SetActive(false);
    }

    private void Start()
    {
        _bullets.text = _weaponManager._currentBullets.ToString() + "/" + _weaponManager._bullets.ToString();
    }

    private void PickWeapon()
    {
        _UIbullets.SetActive(true);
        _bullets.text = _weaponManager._currentBullets.ToString() + "/" + _weaponManager._bullets.ToString();
    }
    private void DropWeapon()
    {
        _UIbullets.SetActive(false);
    }

    private void changeValueBullets()
    {
        _bullets.text = _weaponManager._currentBullets.ToString() + "/" + _weaponManager._bullets.ToString();
    }

}
