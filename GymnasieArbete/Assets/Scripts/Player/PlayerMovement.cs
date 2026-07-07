using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    CapsuleCollider playerCollider;

    [Header("Movement")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    float moveSpeed;

    [SerializeField] float maxStamina;
    [SerializeField] float staminaDrain;
    [SerializeField] float staminaRegen;
    [SerializeField] float staminaCooldown;
    float staminaCooldownTimer;
    float stamina;


    [SerializeField] float groundDrag;

    [Header("Jumping")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    bool readyToJump;

    [Header("Crouching")]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchYScale;
    float startYScale;
    bool isCrouching;



    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask groundLayer;
    bool grounded;

    [SerializeField] Transform orientation;

    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    [Header("State")]
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air,
        crouching
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        isCrouching = false;

        startYScale = transform.localScale.y;

        stamina = maxStamina;
    }

    private void Update()
    {
        Inputs();
        StateHandler();
        GroundCheck();
        Drag();
    }

    private void FixedUpdate()
    {
        Move();
        StaminaControl();
    }

    void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = true;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(crouchKey))
        {
            isCrouching = false;
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    void StateHandler()
    {
        if (Input.GetKey(crouchKey)) // Checks input
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        if (grounded && Input.GetKey(sprintKey) && stamina > 0) // Checks grounded, input and if player has stamina. Needs UI to show stamina
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded && !isCrouching) // Checks grounded and input
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if (!grounded) // If player is in the air
        {
            state = MovementState.air;
        }
    }

    void Move()
    {
        if (state == MovementState.air) return;
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10);

        SpeedControl();
    }

    void SpeedControl()
    {
        // Clamp speed (so it doesn't explode)
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limited = flatVelocity.normalized * moveSpeed; // Readjusts vector to match maxspeed
            rb.linearVelocity = new Vector3(limited.x, rb.linearVelocity.y, limited.z); // Sets vel to maxspeed
        }
    }

    void StaminaControl()
    {
        if (state == MovementState.sprinting && stamina > 0)
        {
            stamina -= staminaDrain * Time.deltaTime;

            // Reset the cooldown every frame while sprinting
            staminaCooldownTimer = staminaCooldown;
        }
        else
        {
            // Count the cooldown down
            if (staminaCooldownTimer > 0)
            {
                staminaCooldownTimer -= Time.deltaTime;
            }
            else
            {
                // Regenerate stamina
                stamina += staminaRegen * Time.deltaTime;
            }
        }

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    void Drag()
    {
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
    }
}
