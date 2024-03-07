using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 1;

    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.up,_speedRotation);
    }
}
