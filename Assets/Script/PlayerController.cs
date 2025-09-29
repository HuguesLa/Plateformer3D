using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 180f; // Degrees per second for rotation

    private Vector2 inputVector; // Store movement input (x for rotation, y for forward/back)
    private Rigidbody rb; // Rigidbody for physics-based movement
    private PlayerInput playerInput; // Reference to PlayerInput component
    private InputAction moveAction; // Input action for movement
    private InputAction jumpAction; // Input action for jumping

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing on " + gameObject.name);
        }

        // Get the PlayerInput component
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component missing on " + gameObject.name);
        }

        // Get references to the input actions
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    // Update is called once per frame
    void Update()
    {
        // Read the movement input as a Vector2
        inputVector = moveAction.ReadValue<Vector2>();

        // Handle rotation based on horizontal input (x-axis)
        float rotationInput = inputVector.x;
        transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f);

        // Handle movement based on forward/back input (y-axis)
        float moveInput = inputVector.y;
        Vector3 moveDirection = transform.forward * moveInput;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Check for jump input
        if (jumpAction.triggered && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Check if the player is grounded to prevent mid-air jumping
    private bool IsGrounded()
    {
        // Adjust the raycast distance based on your player size (1.1f assumes a ~2m tall character)
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // Ensure input actions are cleaned up when the object is destroyed
    void OnDestroy()
    {
        moveAction.Dispose();
        jumpAction.Dispose();
    }
}