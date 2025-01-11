using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;


    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYscale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sneakKey = KeyCode.C;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("State Checks")]
    public bool isSneaking;
    public bool isSprinting;
    public bool isCrouching;
    public bool isWalking;
    public bool isJumping;

    public GroundCheck gCheck;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public bool crouchBob;
    public bool walkBob;
    public bool sprintBob;

    public bool hasJumped;

    public Animator gunAnim;
    public Animator knifeAnim;

    Vector3 moveDirection;

    public Rigidbody rb;

    public MovementState currentState;
    public enum MovementState
    {
        idle,
        walking,
        sprinting,
        crouching,
        sneaking,
        air
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        SpeedControl();
        StateHandler();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);

        if (grounded || OnSlope())
            rb.linearDamping = groundDrag;
        else rb.linearDamping = 0;




    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (readyToJump && grounded && currentState != MovementState.crouching)
        {
            if (Input.GetKey(jumpKey))
            {


                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }

        }
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {

        //crouching
        if (Input.GetKey(crouchKey))
        {
            currentState = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        //sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            currentState = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //sprinting
        else if (grounded && Input.GetKey(sneakKey))
        {
            currentState = MovementState.sneaking;
            moveSpeed = crouchSpeed;
        }

        //walking
        else if (grounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            currentState = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        //air
        else if (!grounded && !OnSlope())
        {
            currentState = MovementState.air;
        }

        else currentState = MovementState.idle;

        //conditionals
        if (currentState == MovementState.crouching)
        {
            isCrouching = true;
            crouchBob = true;
        }
        else
        {
            isCrouching = false;
            crouchBob = false;
        }
        if (currentState == MovementState.sneaking)
        {
            isSneaking = true;
            crouchBob = true;
        }
        else
        {
            isSneaking = false;
            crouchBob = false;
        }
        if (currentState == MovementState.walking)
        {
            isWalking = true;
            walkBob = true;
        }
        else
        {
            isWalking = false;
            walkBob = false;
        }
        if (currentState == MovementState.sprinting)
        {
            isSprinting = true;
            sprintBob = true;
        }
        else
        {
            isSprinting = false;
            sprintBob = false;
        }
        if (currentState == MovementState.sprinting)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //grounded
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
        //in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        //limiting slope speed
        if (OnSlope())
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }
        //limiting speed on ground or air
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        hasJumped = false;

        //reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        hasJumped = true;
        readyToJump = true;
        exitingSlope = false;
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * .5f + .3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

}
