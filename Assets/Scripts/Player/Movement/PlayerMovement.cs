using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float moveMultiplier;
    [SerializeField] private float moveSpeedChange;
    [SerializeField] private float actualSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private bool isSprinting;
    private MovementState movementState;

    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private bool canJump;
    [SerializeField] bool _jump;

    [Header("Crouch Properties")]
    [SerializeField] private float crouchMultiplier;
    [SerializeField] private bool isCrouching;
    [SerializeField] private float crouchCooldown;
    [SerializeField] private bool canCrouch;
    private Vector3 defaultLocalScale;

    [Header("GroundCheck Properties")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 groundCheckBoxSize;
    [SerializeField] private float groundRayDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Gravity Settings")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float airControlReducer;


    [Header("Debug")]
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private bool Grounded;

    #region References

    private Rigidbody rb;
    private PlayerControls playerControl;

    #endregion

    #region Getter / Setter

    /// <summary>
    /// Get is Sprinting Status
    /// </summary>
    /// <returns></returns>
    public bool GetIsSprinting() { return isSprinting; }

    public MovementState GetMovementState() { return movementState; }

    public Rigidbody GetPlayerRB() { return rb; }

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControl = new PlayerControls();
        playerControl.Movement.Enable();

        defaultLocalScale = transform.localScale;
    }

    void Update()
    {
        GetInputs();
        Crouch();
        Sprint();
        SpeedManager();
        Grounded = isGrounded();
    }

    private void FixedUpdate()
    {
        Movement();
        RbDrag();
        Jump();
        LimitVelocity();

        rb.AddForce(Physics.gravity * 3f); //Add extra gravity
    }

    #region Inputs

    /// <summary>
    /// Get Player Inputs
    /// </summary>
    private void GetInputs()
    {
        movementInput = playerControl.Movement.Movement.ReadValue<Vector2>();

        _jump = playerControl.Movement.Jump.IsInProgress();
    }

    #endregion

    #region Movement

    private void Sprint()
    {
        if (playerControl.Movement.Sprint.IsPressed() && isMovingForward(movementInput))
        {
            if (isCrouching) return;

            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    private void SpeedManager()
    {
        if (isCrouching)
        {
            maxSpeed = crouchSpeed;
            movementState = MovementState.crouching;
            actualSpeed = Mathf.Lerp(actualSpeed, crouchSpeed, moveSpeedChange * Time.deltaTime);
        }
        else
        {
            if (isSprinting)
            {
                maxSpeed = runSpeed;
                movementState = MovementState.sprinting;
                actualSpeed = Mathf.Lerp(actualSpeed, runSpeed, moveSpeedChange * Time.deltaTime);
            }
            else
            {
                maxSpeed = walkSpeed;
                movementState = MovementState.walking;
                actualSpeed = Mathf.Lerp(actualSpeed, walkSpeed, moveSpeedChange * Time.deltaTime);
            }
        }
    }

    private void Movement()
    {
        if (movementInput != Vector2.zero)
        {
            Vector2 movement = movementInput.normalized;

            moveDirection = (transform.forward * movement.y + transform.right * movement.x) * actualSpeed;

            if (isGrounded())
            {
                rb.AddForce(moveDirection * moveMultiplier, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(moveDirection * moveMultiplier * airControlReducer, ForceMode.Acceleration);
            }
        }
    }

    private IEnumerator jumpCoroutine()
    {
        yield return new WaitForSeconds(jumpCooldown);

        canJump = true;
    }

    private void Jump()
    {
        if (_jump && isGrounded() && canJump)
        {
            rb.velocity = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            _jump = false;

            StartCoroutine(jumpCoroutine());
        }
    }

    private IEnumerator crouchCoroutine()
    {
        yield return new WaitForSeconds(crouchCooldown);
        canCrouch = true;
    }

    private void Crouch()
    {
        if (playerControl.Movement.Crouch.WasPressedThisFrame() && canCrouch)
        {
            isCrouching = true;
            canCrouch = false;

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * crouchMultiplier, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (playerControl.Movement.Crouch.WasReleasedThisFrame() && isCrouching)
        {
            isCrouching = false;
            transform.localScale = defaultLocalScale;

            StartCoroutine(crouchCoroutine());
        }
    }

    private void RbDrag()
    {
        if (isGrounded()) rb.drag = groundDrag;
        else rb.drag = airDrag;
    }

    private void LimitVelocity()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 limitedVel = flatVel.normalized * maxSpeed;

        // limit velocity if needed
        if (flatVel.magnitude > maxSpeed)
        {
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    #endregion

    #region Checkers

    /// <summary>
    /// Detect if player is moving forward
    /// </summary>
    /// <param name="movement"></param>
    /// <returns></returns>
    private bool isMovingForward(Vector2 movement)
    {
        return movement.y > 0 && rb.velocity.magnitude > 0.1f;
    }

    /// <summary>
    /// Detect if player is grounded
    /// </summary>
    /// <returns></returns>
    private bool isGrounded()
    {
        return gameObject.scene.GetPhysicsScene().BoxCast(groundCheck.position, groundCheckBoxSize, Vector3.down, out RaycastHit hit, Quaternion.identity, groundRayDistance, groundLayer);
    }

    #endregion

    #region Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize); //Draw box check
    }
    #endregion
}
