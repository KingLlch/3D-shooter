using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RayCastController _rayCastController;
    private SoundManager _soundManager;
    private WeaponManager _weaponManager;

    private Camera _camera;
    private GameObject _head;
    private GameObject _item, _weapon;

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
    private bool _isPickUpWeapon;

    [HideInInspector] public UnityEvent Shot; 
    [HideInInspector] public UnityEvent PickWeapon;
    [HideInInspector] public UnityEvent DropWeapon;
    [HideInInspector] public UnityEvent ChangeValueBullets;

    private void Awake()
    {
        _rayCastController = gameObject.GetComponent<RayCastController>();
        _soundManager = GameObject.FindObjectOfType<SoundManager>();
        _weaponManager = GameObject.FindObjectOfType<WeaponManager>();

        _camera = Camera.main;
        _head = GameObject.Find("Head");
    }

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

        if ((Input.GetKeyDown(KeyCode.E) && (_isPickUpItem == false)) || ((Input.GetKeyDown(KeyCode.E) && (_isPickUpWeapon == false))))
        {
            if (_rayCastController._rayCastHit.collider.gameObject.GetComponent<PickUpItem>() && _rayCastController.Distance <= 2f)
            {
                _rayCastController._rayCastHit.collider.gameObject.GetComponent<PickUpItem>().PickUp();
                _item = _rayCastController._rayCastHit.collider.gameObject;
                _isPickUpItem = true;
            }

            if (_rayCastController._rayCastHit.collider.gameObject.GetComponent<PickUpWeapon>() && _rayCastController.Distance <= 2f)
            {
                _rayCastController._rayCastHit.collider.gameObject.GetComponent<PickUpWeapon>().PickUp();
                _weapon = _rayCastController._rayCastHit.collider.gameObject;
                _isPickUpWeapon = true;
                PickWeapon.Invoke();
            }

        }

        if (Input.GetKeyDown(KeyCode.Q) && (_isPickUpItem == true))
        {
            _item.GetComponent<PickUpItem>().PickOff();
            _isPickUpItem = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && (_isPickUpWeapon == true))
        {
            _weapon.GetComponent<PickUpWeapon>().PickOff();
            _isPickUpWeapon = false;
            DropWeapon.Invoke();
        }

        if (Input.GetMouseButtonDown(0) && (_isPickUpWeapon == true))
        {
            _weaponManager.shot();
            ChangeValueBullets.Invoke();
            Shot.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R) && (_isPickUpWeapon == true))
        {
            _weaponManager.reload();
            _soundManager.PistolReload();
            ChangeValueBullets.Invoke();
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
            //_velocity.y = -5;
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
