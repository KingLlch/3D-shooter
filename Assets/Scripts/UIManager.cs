using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private PickUpController _pickUpController;
    private WeaponManager _weaponManager;

    private GameObject _bulletsUI;
    private TextMeshProUGUI _bullets;

    private void Awake()
    {
        _bulletsUI = GameObject.Find("BulletsUI");
        _bullets = GameObject.Find("Bullets").GetComponent<TextMeshProUGUI>();
        _pickUpController = GameObject.FindObjectOfType<PickUpController>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();

        _pickUpController.PickUpWeapon.AddListener(PickWeapon);
        _pickUpController.PickOffWeapon.AddListener(DropWeapon);
        _weaponManager.ShotWithPatrons.AddListener(ChangeValueBullets);
        _weaponManager.Reload.AddListener(ChangeValueBullets);

        _bulletsUI.SetActive(false);
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

}
