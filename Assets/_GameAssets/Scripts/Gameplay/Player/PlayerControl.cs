using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public event Action OnPlayerJumped;
    public event Action<PlayerState> OnPlayerStateChanged;

    [Header("References")]
    [SerializeField] private Transform Orientation;

    [Header("MovementSettings")]
    [SerializeField] private float MovementSpeed;
    [SerializeField] private KeyCode movementKey;

    [Header("JumpSettings")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    [SerializeField] private float jumpCoolDown;

    [Header("Ground Check Settings")]
    [SerializeField] private float playerheight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float GroundDrag;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMultiplier;
    [SerializeField] private float SlideDrag;

    private StateController _stateController;
    private Rigidbody PlayerRb;
    private float _startingMovementSpeed, _startingJumpForce;
    private float horizontalInput, verticalInput;
    private Vector3 _MovementDirection;
    private bool isSliding;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        PlayerRb = GetComponent<Rigidbody>();
        PlayerRb.freezeRotation = true;
        _startingMovementSpeed = MovementSpeed;
        _startingJumpForce = jumpForce;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
             && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        PlayerMovement();
    }

    private void SetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey))
        {
            isSliding = true;
        }
        else if (Input.GetKeyDown(movementKey))
        {
            isSliding = false;
        }
        else if (Input.GetKey(jumpKey) && canJump && IsGrounded())
        {
            canJump = false;
            PlayerJump();
            Invoke(nameof(canJumpReset), jumpCoolDown);
        }
    }

    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var _issliding = IsSliding();
        var CurrentState = _stateController.GetCurrentState();

        var newState = CurrentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !_issliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !_issliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && _issliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && _issliding => PlayerState.SlideIdle,
            _ when !canJump && !isGrounded => PlayerState.Jump,
            _ => CurrentState
        };

        if (newState != CurrentState)
        {
            _stateController.ChangeState(newState);
            OnPlayerStateChanged?.Invoke(newState);
        }
    }

    private void PlayerMovement()
    {
        _MovementDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        float forcemultiplayer = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };

        PlayerRb.AddForce(_MovementDirection.normalized * MovementSpeed * forcemultiplayer, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        PlayerRb.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => GroundDrag,
            PlayerState.Slide => SlideDrag,
            PlayerState.Jump => _airDrag,
            _ => PlayerRb.linearDamping
        };
    }
    private void LimitPlayerSpeed()
    {
        Vector3 flatvelocity = new Vector3(PlayerRb.linearVelocity.x, 0f, PlayerRb.linearVelocity.z);

        if (flatvelocity.magnitude > MovementSpeed)
        {
            Vector3 limitedVelocity = flatvelocity.normalized * MovementSpeed;
            PlayerRb.linearVelocity = new Vector3(limitedVelocity.x, PlayerRb.linearVelocity.y, limitedVelocity.z);

        }
    }

    private void PlayerJump()
    {
        OnPlayerJumped?.Invoke();
        PlayerRb.linearVelocity = new Vector3(PlayerRb.linearVelocity.x, 0f, PlayerRb.linearVelocity.z);
        PlayerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void canJumpReset()
    {
        canJump = true;
    }

    #region Helper Functions

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return _MovementDirection.normalized;
    }

    private bool IsSliding()
    {
        return isSliding;
    }

    public void SetMovementSpeed(float speed, float duration)
    {
        MovementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }
    private void ResetMovementSpeed()
    {
        MovementSpeed = _startingMovementSpeed;
    }
    public void SetJumpForce(float force, float duration)
    {
        jumpForce += force;
        Invoke(nameof(ResetjumpedForce), duration);
    }
    private void ResetjumpedForce()
    {
        jumpForce = _startingJumpForce;
    }

    public Rigidbody GetPlayerRigidbody()
    {
        return PlayerRb;
    }

    public bool CanCatChase()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit,
            playerheight * 0.5f + 0.2f, groundLayer))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Consts.Layers.FLOOR_LAYER))
            {
                return true;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Consts.Layers.FLOOR_LAYER))
            {
                return false;
            }
        }
        return false;
    }
    #endregion
}
