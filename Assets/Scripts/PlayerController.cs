using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeedCofficient;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _playerHigh;

    private float _collisionTimer;
    private float _speed;
    private float _moveHorizontal;
    private float _moveVertical;

    private Vector3 _velocity;
    private Vector3 _headRotate;

    private bool _isJumping;
    private bool _isGrounded;
    private bool _isMultiShooting = false;

    [HideInInspector] public UnityEvent ShotButtonDownSingle;
    [HideInInspector] public UnityEvent ShotButtonDownMulti;
    [HideInInspector] public UnityEvent ReloadButtonDown;
    [HideInInspector] public UnityEvent ChangeTypeShot;
    [HideInInspector] public UnityEvent PickWeaponOrItemButtonDown;
    [HideInInspector] public UnityEvent DropWeaponButtonDown;
    [HideInInspector] public UnityEvent DropItemButtonDown;
    [HideInInspector] public UnityEvent CollisionWithEnemy;
    [HideInInspector] public UnityEvent CollisionWithMedpack;
    [HideInInspector] public UnityEvent CollisionWithAmmo;
    [HideInInspector] public UnityEvent EscapeButtonDown;

    [HideInInspector] public GameObject Head;
    [HideInInspector] public GameObject Enemy;
    [HideInInspector] public GameObject Medpack;
    [HideInInspector] public GameObject Ammopack;
    [HideInInspector] public bool IsPaused = false;

    private void Awake()
    {
        Head = GameObject.Find("Head");
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _speed = _moveSpeed;
    }

    private void Update()
    {
        if (IsPaused) return;

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * gameObject.GetComponent<Rigidbody>().velocity.y + transform.TransformDirection(_velocity);

        if (_isMultiShooting)
        {
            ShotButtonDownMulti.Invoke();
        }
    }

    //new input system
    public void EscapeMethod()
    {
        EscapeButtonDown.Invoke();
    }

    public void MoveMethod(InputAction.CallbackContext move)
    {
        if (IsPaused) return;

        if (move.performed)
        {
            _velocity = Vector3.up * _velocity.y;

            _moveVertical = move.ReadValue<Vector2>().y;
            _moveHorizontal = move.ReadValue<Vector2>().x;

            _velocity = new Vector3(_moveHorizontal * _speed, _velocity.y, _moveVertical * _speed);
        }

        else if (move.canceled)
        {
            _velocity = new Vector3(0, _velocity.y, 0);
        }
    }

    public void LookMethod(InputAction.CallbackContext rotate)
    {
        if (IsPaused) return;

        gameObject.transform.Rotate(Vector3.up * rotate.ReadValue<Vector2>().x * _rotationSpeed);

        _headRotate = Head.transform.eulerAngles;
        _headRotate.x -= rotate.ReadValue<Vector2>().y * _rotationSpeed;
        _headRotate.x = LockRotation(_headRotate.x);
        Head.transform.eulerAngles = _headRotate;
    }

    public void JumpMethod()
    {
        if (IsPaused) return;

        if (_isGrounded == true)
        {
            if (_isJumping == false)
            {
                _velocity.y = _jumpSpeed;
                _isJumping = true;
            }
        }
    }

    public void HasteMethod(InputAction.CallbackContext pressed)
    {
        if (IsPaused) return;

        if (pressed.performed)
        {
            _speed = _moveSpeed * _runSpeedCofficient;
        }

        else if (pressed.canceled)
        {
            _speed = _moveSpeed;
        }
    }

    public void ChangeTypeShotMethod()
    {
        if (IsPaused) return;
        _isMultiShooting = false;
        ChangeTypeShot.Invoke();
    }

    public void ReloadMethod()
    {
        if (IsPaused) return;
        _isMultiShooting = false;
        ReloadButtonDown.Invoke();
    }

    public void ShotButtonDownSingleMethod(InputAction.CallbackContext pressed)
    {
        if (IsPaused) return;
        if (pressed.performed) ShotButtonDownSingle.Invoke();
    }

    public void ShotButtonDownMultiMethod(InputAction.CallbackContext pressed)
    {
        if (IsPaused) return;
        if (pressed.performed) _isMultiShooting = true;
        else _isMultiShooting = false;
    }

    public void DropWeaponButtonDownMethod()
    {
        if (IsPaused) return;
        DropWeaponButtonDown.Invoke();
    }

    public void DropItemButtonDownMethod()
    {
        if (IsPaused) return;
        DropItemButtonDown.Invoke();
    }

    public void PickWeaponOrItemButtonDownMethod()
    {
        if (IsPaused) return;
        PickWeaponOrItemButtonDown.Invoke();
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
            _velocity.y = 0;
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

    private void OnCollisionStay(Collision collision)
    {
        _collisionTimer += Time.fixedDeltaTime;

        if ((collision.gameObject.tag == "Enemy") && (_collisionTimer >= 1))
        {
            Enemy = collision.gameObject;
            CollisionWithEnemy.Invoke();
            _collisionTimer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Medpack>() == true)
        {
            Medpack = collision.gameObject;
            CollisionWithMedpack.Invoke();
        }

        if (collision.gameObject.GetComponent<Ammo>() == true)
        {
            Ammopack = collision.gameObject;
            CollisionWithAmmo.Invoke();
        }
    }
}
