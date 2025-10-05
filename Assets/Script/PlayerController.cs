using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 180f;

    private Vector2 inputVector;
    private Rigidbody rb; 
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing on " + gameObject.name);
        }

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component missing on " + gameObject.name);
        }

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        inputVector = moveAction.ReadValue<Vector2>();

        float rotationInput = inputVector.x;
        transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f);

        float moveInput = inputVector.y;
        Vector3 moveDirection = transform.forward * moveInput;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (jumpAction.triggered && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void OnDestroy()
    {
        moveAction.Dispose();
        jumpAction.Dispose();
    }
}