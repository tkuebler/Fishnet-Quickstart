using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputDriver : NetworkBehaviour
{
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector3 _moveDirection;
    private bool _jump;
    [SerializeField] public float jumpSpeed = 6f;
    [SerializeField] public float speed = 8f;
    [SerializeField] public float gravity = 9.8f;
    private void Start()
    {
        _characterController = GetComponent(typeof(CharacterController)) as CharacterController;
        _jump = false;
    }
    private void Update()
    {
        if (!base.IsOwner)
            return;
        if (_characterController.isGrounded)
        {
            _moveDirection = new Vector3(_moveInput.x, 0.0f, _moveInput.y);
            _moveDirection *= speed;

            if (_jump)
            {
                _moveDirection.y = jumpSpeed;
                _jump = false;
            }
        }
        _moveDirection.y -= gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
    #region UnityEventCallbacks
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!base.IsOwner)
            return;
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!base.IsOwner)
            return;
        if (context.started || context.performed)
        {
            _jump = true;
        }
        else if (context.canceled)
        {
            _jump = false;
        }
    }
    #endregion
}
