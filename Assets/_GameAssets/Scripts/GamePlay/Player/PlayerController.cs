using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
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
    [SerializeField] private bool canJump;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode slideKey;
    [SerializeField] private float slideMultiplier;
    [SerializeField] private float slideDrag;

    [Header("Ground Settings")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag;
    

    private float horizontalInput, verticalInput;

    private Vector3 movementDirection;

    private bool isSliding;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
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
        else if(Input.GetKeyDown(movementKey))
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

    private void SetPlayerMovement()
    {
        movementDirection = orientationTransform.forward * verticalInput + orientationTransform.right * horizontalInput;
        playerRigidbody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        if (isSliding)
        {
            playerRigidbody.linearDamping = slideDrag;
        }
        else
        {
            playerRigidbody.linearDamping = groundDrag;
        }
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
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        if (isSliding)
        {
            playerRigidbody.AddForce(movementDirection.normalized * movementSpeed * slideMultiplier, ForceMode.Impulse);
        }
        else
        {
            playerRigidbody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Impulse);
        }
    }

    private void ResetJumping()
    {
        canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
    }


}
