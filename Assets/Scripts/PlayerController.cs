using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RayCastController _rayCastController;

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _head;

     private GameObject _item;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeedCofficient;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _playerHigh;

    private float _speed;
    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _velocity;
    private Vector3 _headRotate;
    private bool _isJumping;
    private bool _isGrounded;
    private bool _isPickUpItem;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        _velocity = Vector3.up * _velocity.y;

        _moveVertical = Input.GetAxisRaw("Vertical");
        _moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _moveSpeed * _runSpeedCofficient;
        }
        else
        {
            _speed = _moveSpeed;
        }

        _velocity = new Vector3(_moveHorizontal * _speed, _velocity.y, _moveVertical * _speed);

        gameObject.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * _rotationSpeed);

        _headRotate = _head.transform.eulerAngles;
        _headRotate.x -= Input.GetAxis("Mouse Y") * _rotationSpeed;
        _headRotate.x = LockRotation(_headRotate.x);
        _head.transform.eulerAngles = _headRotate;

        if (_isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && (_isJumping == false))
            {
                _velocity.y = _jumpSpeed;
                _isJumping = true;
            }
        }
        gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(_velocity);

        if (Input.GetKeyDown(KeyCode.E) && (_isPickUpItem == false)) 
        {
            if (_rayCastController._rayCastHit.collider.gameObject.GetComponent<PickUpItem>())
            {
                _rayCastController._rayCastHit.collider.gameObject.GetComponent<PickUpItem>().PickUp();
                _item = _rayCastController._rayCastHit.collider.gameObject;
                _isPickUpItem = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Q) && (_isPickUpItem == true))
        {
            _item.GetComponent<PickUpItem>().PickOff();
            _isPickUpItem = false;
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, _playerHigh))
        {
            _isGrounded = true;
            _isJumping = false;
        }

        else
        {
            _isGrounded = false;
            _velocity.y = -2;
        }
    }

    private float LockRotation(float angle, float angleMax = 90f, float angleMin = -90f)
    {
        if (angle > 180) angle -= 360;
        if (angle < -180) angle += 360;

        if (angle > angleMax) angle = angleMax;
        if (angle < angleMin) angle = angleMin;

        return angle;
    }
}
