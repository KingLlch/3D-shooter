using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    private PickUpController _pickUpController;
    private WeaponManager _weaponManager;

    private GameObject _UIbullets;
    private TextMeshProUGUI _bullets;

    private void Awake()
    {
        _UIbullets = GameObject.Find("BulletsUI");
        _bullets = GameObject.Find("Bullets").GetComponent<TextMeshProUGUI>();
        _pickUpController = GameObject.FindObjectOfType<PickUpController>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();

        _pickUpController.PickUpWeapon.AddListener(PickWeapon);
        _pickUpController.PickOffWeapon.AddListener(DropWeapon);
        _weaponManager.ShotWithPatrons.AddListener(ChangeValueBullets);
        _weaponManager.Reload.AddListener(ChangeValueBullets);

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

    private void ChangeValueBullets()
    {
        _bullets.text = _weaponManager._currentBullets.ToString() + "/" + _weaponManager._bullets.ToString();
    }

}
