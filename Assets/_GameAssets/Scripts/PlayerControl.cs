using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Orientation;

    [Header("MovementSettings")]
    [SerializeField] private float MovementSpeed;
    [SerializeField] private KeyCode movementKey;

    [Header("JumpSettings")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpCoolDown;

    [Header("Ground Check Settings")]
    [SerializeField] private float playerheight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float GroundDrag;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMultiplier;
    [SerializeField] private float SlideDrag;

    private Rigidbody PlayerRb;
    private float horizontalInput, verticalInput;
    private Vector3 MovementDirection;
    private bool isSliding;

    private void Awake()
    {
        PlayerRb = GetComponent<Rigidbody>();
        PlayerRb.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
        SetPlayerDrag();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void SetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey))
        {
            isSliding = true;
            Debug.Log("Hýzlý");
        }
        else if (Input.GetKeyDown(movementKey)) 
        {
           isSliding= false;
            Debug.Log("Normal");
        }
        else if (Input.GetKey(jumpKey) && canJump && isGrounded())
        {
            canJump = false;
            PlayerJump();
            Invoke(nameof(canJumpReset),jumpCoolDown);
        }
    }

    private void PlayerMovement()
    {
        MovementDirection = Orientation.forward * verticalInput+ Orientation.right* horizontalInput;


        if (isSliding)
        {
            PlayerRb.AddForce(MovementDirection.normalized * MovementSpeed * slideMultiplier, ForceMode.Force);
        }
        else
        {
            PlayerRb.AddForce(MovementDirection.normalized * MovementSpeed, ForceMode.Force);
        }
    }

    private void SetPlayerDrag()
    {
        if (isSliding)
        {
            PlayerRb.linearDamping = SlideDrag;
        }
        else
        {
            PlayerRb.linearDamping = GroundDrag;
        }

    }
    private void LimitPlayerSpeed()
    {
        Vector3 flatvelocity = new Vector3(PlayerRb.linearVelocity.x, 0f, PlayerRb.linearVelocity.z);
    }

    private void PlayerJump()
    {
        PlayerRb.linearVelocity = new Vector3(PlayerRb.linearVelocity.x, 0f, PlayerRb.linearVelocity.z);
        PlayerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void canJumpReset()
    {
        canJump = true;
    }
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, groundLayer);
    }
}
