using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PickUpWeapon : MonoBehaviour
{
    private GameObject WeaponHand;

    private void Awake()
    {
        WeaponHand = GameObject.Find("WeaponHand");
    }

    public void PickUp()
    {
        gameObject.transform.SetParent(WeaponHand.transform, true);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

    }

    public void PickOff()
    {
        gameObject.transform.SetParent(null, true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
