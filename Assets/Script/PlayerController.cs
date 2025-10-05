using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float runSpeed = 8f; // Faster speed for running
    public float jumpForce = 7f; // Slightly increased for responsiveness
    public float rotationSpeed = 180f;

    // Dash variables
    public float dashForce = 30f; // Force for dash
    public float dashDuration = 0.2f; // Duration of dash impulse
    public float dashCooldown = 2f; // Cooldown time between dashes
    private float lastDashTime = -Mathf.Infinity;
    private float dashTimer = 0f;
    private bool isDashing = false;

    // Stamina variables for running
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 10f;
    private float currentStamina;

    private Vector2 inputVector;
    private bool jumpTriggered;
    private bool dashTriggered;
    private float runInput;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;
    private InputAction runAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing on " + gameObject.name);
        }

        // Optimize Rigidbody settings
        rb.linearDamping = 2f; // Reduced drag to allow faster falling
        rb.angularDamping = 5f;
        rb.useGravity = true; // Ensure gravity is enabled
        rb.mass = 1f; // Standard mass
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component missing on " + gameObject.name);
        }

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        dashAction = playerInput.actions["Dash"];
        runAction = playerInput.actions["Run"];

        currentStamina = maxStamina;
    }

    void Update()
    {
        // Read inputs in Update
        inputVector = moveAction.ReadValue<Vector2>();
        jumpTriggered |= jumpAction.triggered;
        dashTriggered |= dashAction.triggered;
        runInput = runAction.ReadValue<float>();

        // Stamina management
        bool isRunning = runInput > 0f && currentStamina > 0f && Mathf.Abs(inputVector.y) > 0f;
        if (isRunning)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    void FixedUpdate()
    {
        // Rotation
        float rotationInput = inputVector.x;
        transform.Rotate(0f, rotationInput * rotationSpeed * Time.fixedDeltaTime, 0f);

        // Determine speed
        bool isRunning = runInput > 0f && currentStamina > 0f;
        float currentSpeed = isRunning ? runSpeed : moveSpeed;

        // Horizontal movement
        float moveInput = inputVector.y;
        Vector3 moveDirection = transform.forward * moveInput * currentSpeed;
        Vector3 newVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        if (!isDashing)
        {
            rb.linearVelocity = newVelocity;
        }

        // Jump logic
        if (jumpTriggered && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpTriggered = false;
        }

        // Dash logic
        if (dashTriggered && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
            lastDashTime = Time.time;
            dashTriggered = false;
        }

        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer > 0f)
            {
                Vector3 dashVelocity = transform.forward * dashForce;
                rb.linearVelocity = new Vector3(dashVelocity.x, rb.linearVelocity.y, dashVelocity.z);
            }
            else
            {
                isDashing = false;
            }
        }

        // Stop horizontal movement when no input to prevent sliding
        if (Mathf.Abs(moveInput) < 0.01f && IsGrounded() && !isDashing)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }

        // Ensure gravity is applied correctly
        if (!IsGrounded() && !isDashing)
        {
            rb.AddForce(Physics.gravity * 1.5f, ForceMode.Acceleration); // Boost gravity when falling
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.2f);
    }

    public float GetStaminaPercentage()
    {
        return currentStamina / maxStamina;
    }

    void OnDestroy()
    {
        moveAction.Dispose();
        jumpAction.Dispose();
        dashAction.Dispose();
        runAction.Dispose();
    }
}