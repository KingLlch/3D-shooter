using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private GameObject Hand;

    private void Awake()
    {
        Hand = GameObject.Find("Hand");
    }

    public void PickUp()
    {
        gameObject.transform.SetParent(Hand.transform, true);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    public void PickOff()
    {
        gameObject.transform.SetParent(null, true);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
