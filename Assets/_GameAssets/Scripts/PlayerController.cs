using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    [SerializeField] private Transform orientationTransform;
    [SerializeField] private float movementSpeed = 20f;

    private float horizontalInput, verticalInput;

    private Vector3 movementDirection;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void SetPlayerMovement()
    {
        movementDirection = orientationTransform.forward * verticalInput + orientationTransform.right * horizontalInput;
        playerRigidbody.AddForce(movementDirection * movementSpeed, ForceMode.Force);
    }


}
