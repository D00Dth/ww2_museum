using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private Vector2 mouseSensitivity = Vector2.one;
    [SerializeField] private Transform camera;
    [SerializeField] private CursorManager cursorManager;

    [SerializeField] private bool escapePerformed;


    [SerializeField] private Vector2 moveInputs, lookInputs;
    [SerializeField] private bool jumpPerformed;
    [SerializeField] private CharacterController characterController;

    private Vector3 velocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Look();
    }


    void FixedUpdate()
    {
        if(cursorManager.isLocked)
        {
            Vector3 _horizontalVelocity = speed * new Vector3(moveInputs.x, 0, moveInputs.y);
            float _gravityVelocity = GravityForce(velocity.y);

            velocity = _horizontalVelocity + _gravityVelocity * Vector3.up;

            TryJump();

            Vector3 _motion = transform.right * velocity.x + transform.up * velocity.y + transform.forward * velocity.z;

            characterController.Move(_motion * Time.deltaTime);
        }
    }

    public float GravityForce(float _verticalVelocity)
    {
        if(characterController.isGrounded) return 0f;

        _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        return _verticalVelocity;
    }

    public void TryJump()
    {
        if(cursorManager.isLocked)
        {
            if(!jumpPerformed || !characterController.isGrounded) return;

            velocity.y += jumpForce;
            jumpPerformed = false;
        }
    }

    public void Look()
    {
        if(cursorManager.isLocked)
        {
            transform.Rotate(lookInputs.x * mouseSensitivity.x * Time.deltaTime * Vector3.up);
        
            float _cameraAngleX = camera.localEulerAngles.x - lookInputs.y * Time.deltaTime * mouseSensitivity.y;
            if(_cameraAngleX <= 90f) _cameraAngleX = _cameraAngleX > 0 ? Mathf.Clamp(_cameraAngleX, 0f, 89.5f) : _cameraAngleX;
            if(_cameraAngleX >= 270f) _cameraAngleX = Mathf.Clamp(_cameraAngleX, 270.5f, 360f);

            camera.localEulerAngles = Vector3.right * _cameraAngleX;
        }

    }

    public void MovePerformed(InputAction.CallbackContext _ctx) => moveInputs = _ctx.ReadValue<Vector2>();
    public void LookPerformed(InputAction.CallbackContext _ctx) => lookInputs = _ctx.ReadValue<Vector2>();
    public void JumpPerformed(InputAction.CallbackContext _ctx) => jumpPerformed = _ctx.performed;
}
