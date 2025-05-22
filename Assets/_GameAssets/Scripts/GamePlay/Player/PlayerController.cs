using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJumped;
    public event Action<PlayerState> OnPlayerStateChanged;

    private Rigidbody playerRigidbody;

    [Header("References")]
    [SerializeField] private Transform orientationTransform;

    [Header("Movement Settings")]
    [SerializeField] private KeyCode movementKey;
    [SerializeField] private float movementSpeed = 20f;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float airDrag;
    [SerializeField] private bool canJump;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMultiplier;
    [SerializeField] private float slideDrag;

    [Header("Ground Settings")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag;

    private StateController stateController;

    private float starintgMovementSpeed, startingJumpForce;
    private float horizontalInput, verticalInput;

    private Vector3 movementDirection;

    private bool isSliding;

    private void Awake()
    {
        stateController = GetComponent<StateController>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;

        starintgMovementSpeed = movementSpeed;
        startingJumpForce = jumpForce;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurentGameState() != GameState.Play && GameManager.Instance.GetCurentGameState() != GameState.Resume)
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
        if (GameManager.Instance.GetCurentGameState() != GameState.Play && GameManager.Instance.GetCurentGameState() != GameState.Resume)
        {
            return;
        }
        SetPlayerMovement();
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
            // Jump
            canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), jumpCooldown);
        }
    }

    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var currentState = stateController.GetCurrentState();
        var isSliding = IsSliding();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            stateController.ChangeState(newState);
            OnPlayerStateChanged?.Invoke(newState);
        }

        Debug.Log(newState);

    }

    private void SetPlayerMovement()
    {
        movementDirection = orientationTransform.forward * verticalInput + orientationTransform.right * horizontalInput;

        float forceMultiplier = stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => slideMultiplier,
            PlayerState.Jump => airMultiplier,
            _ => 1f
        };

        playerRigidbody.AddForce(movementDirection.normalized * movementSpeed * forceMultiplier, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {

        playerRigidbody.linearDamping = stateController.GetCurrentState() switch
        {
            PlayerState.Move => groundDrag,
            PlayerState.Slide => slideDrag,
            PlayerState.Jump => airDrag,
            _ => playerRigidbody.linearDamping
        };

    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, 0f, limitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        OnPlayerJumped?.Invoke();

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);

        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        canJump = true;
    }

    #region Helper Functions

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return isSliding;
    }

    public void SetMovementSpeed(float speed, float duration)
    {
        movementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }

    private void ResetMovementSpeed()
    {
        movementSpeed = starintgMovementSpeed;
    }

    public void SetJumpForce(float force, float duration)
    {
        jumpForce += force;
        Invoke(nameof(ResetJumpForce), duration);
    }

    private void ResetJumpForce()
    {
        jumpForce = startingJumpForce;
    }

    public Rigidbody GetPlayerRigidbody()
    {
        return playerRigidbody;
    }

    public bool CanCatChase()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.2f, groundLayer))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Const.Layers.FLOOR_LAYER))
            {
                return true;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Const.Layers.GROUND_LAYER))
            {
                return false;
            }
        }
        return false;
    }
    #endregion


}
