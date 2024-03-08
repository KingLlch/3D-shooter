using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    private Camera _camera;

    public float Distance;
    public RaycastHit _rayCastHit;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void LateUpdate()
    {
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out _rayCastHit);
        Distance = _rayCastHit.distance;
    }
}
