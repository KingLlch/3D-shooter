using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
    private GameObject _head;
    private Camera _camera;

    public float Distance;
    public RaycastHit _rayCastHit;

    private void Awake()
    {
        _head = GameObject.Find("Head");
        _camera = Camera.main;
    }
    private void LateUpdate()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out _rayCastHit);
        Distance = _rayCastHit.distance;

        //_point.transform.position = _rayCastHit.point;

        //_rayCastHit.collider.gameObject.GetComponent<Texture>;
    }
}
