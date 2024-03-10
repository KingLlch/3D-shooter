using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 1;
    public Vector3 RotationVector = Vector3.up;
    void FixedUpdate()
    {
        gameObject.transform.Rotate(RotationVector, _speedRotation);
    }
}
