using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference movementAction;
    public InputActionReference jumpAction;

    private Rigidbody2D playerRigidBody;
    private GroundCheck groundCheck;

    private Vector2 movementInput = Vector2.zero;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float breakForce;
    private float excessSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float coyoteTime;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private bool isGrounded;
    [SerializeField] private float airDrag;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        movementInput = movementAction.action.ReadValue<Vector2>();

        isGrounded = groundCheck.HandleGroundCheck();

        // Track grounded time (coyote)
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Track jump buffer time
        if (jumpAction.action.WasPressedThisFrame())
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        AccelerationInAir();
    }

    private void OnEnable()
    {
        movementAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        movementAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Jump()
    { 
        if(coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            playerRigidBody.linearVelocityY += jumpForce;

            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
        HandleGravity();
        Jump();
    }

    private void HandlePlayerMovement()
    {
        if(movementInput.magnitude > 0.1f)
        {
            if (Mathf.Abs(playerRigidBody.linearVelocityX) < baseSpeed)
                playerRigidBody.linearVelocityX = baseSpeed * Mathf.Sign(movementInput.x);
            playerRigidBody.linearVelocityX += movementInput.x * acceleration * Time.deltaTime;

            playerRigidBody.linearVelocityX = Mathf.Clamp(playerRigidBody.linearVelocityX, -maxSpeed, maxSpeed);
        }
        else if(playerRigidBody.linearVelocityX != 0)
        {
            playerRigidBody.linearVelocityX = Mathf.MoveTowards(playerRigidBody.linearVelocityX, 0f, deceleration * Time.deltaTime);
        }
        else
        {
            playerRigidBody.linearVelocityX = 0f;
        }
    }

    /*private void HandlePlayerMovementV2()
    {
        Debug.DrawRay(transform.position, playerRigidBody.linearVelocity.normalized, Color.blue);
        Debug.Log(playerRigidBody.linearVelocityX);

        if (movementInput.magnitude > 0.1f)
        {
            if (Math.Abs(playerRigidBody.linearVelocityX) < baseSpeed)
            {
                playerRigidBody.AddForceX(Mathf.Sign(movementInput.x) * baseSpeed, ForceMode2D.Impulse);
            }

            playerRigidBody.AddForceX(movementInput.x * acceleration, ForceMode2D.Force);

            float alignment = Vector2.Dot(playerRigidBody.linearVelocity.normalized, movementInput.normalized);

            if (Mathf.Abs(playerRigidBody.linearVelocityX) >= maxSpeed && alignment > 0.75f)
            {
                excessSpeed = Mathf.Abs(playerRigidBody.linearVelocityX) - maxSpeed;
                ApplyBrake(excessSpeed);
            }

            else if (alignment < -0.75f)
            {
                excessSpeed = Mathf.Abs(playerRigidBody.linearVelocityX);
                ApplyBrake(excessSpeed);
            }
        }

        else
        {
            if (playerRigidBody.linearVelocityX != 0)
            {
                excessSpeed = Mathf.Abs(playerRigidBody.linearVelocityX);
                ApplyBrake(excessSpeed);
            }
        }
    }*/

    private void ApplyBrake(float speed)
    {
        if (Mathf.Abs(playerRigidBody.linearVelocityX) > 0.01f)
        {
            float dir = Mathf.Sign(playerRigidBody.linearVelocityX);
            playerRigidBody.AddForceX(-dir * speed * breakForce, ForceMode2D.Force);
        }
        else 
            playerRigidBody.linearVelocityX = 0f;
    }


    private void AccelerationInAir()
    {
        if(!isGrounded)
        {
            acceleration *= airDrag;
        }

        else
        {
            acceleration = 60;
        }
    }

    private void HandleGravity()
    {
        if(playerRigidBody.linearVelocityY > 0f && jumpAction.action.IsPressed())
        {
            playerRigidBody.gravityScale = 5;
        }

        else
        {
            playerRigidBody.gravityScale = 10;
        }

        /*if (!jumpIsHeld && playerRigidBody.linearVelocityY < 0f)
        {
            playerRigidBody.gravityScale = 10;
        }
        else
        {
            playerRigidBody.gravityScale = 6;
        }*/
    }
}
