using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private GameObject _point;
    [SerializeField] private Camera _camera;

    public RaycastHit _rayCastHit;

    private void LateUpdate()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out _rayCastHit);
        //_point.transform.position = _rayCastHit.point;

        //_rayCastHit.collider.gameObject.GetComponent<Texture>;
    }
}
