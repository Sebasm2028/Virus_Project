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
    [SerializeField] private float moveMultiplier;
    [SerializeField] private float moveSpeedChange;
    [SerializeField] private float actualSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private bool isSprinting;

    [Header("GroundCheck Properties")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 groundCheckBoxSize;
    [SerializeField] private float groundRayDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Gravity Settings")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float airControlReducer;

    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private bool canJump;
    [SerializeField] bool _jump;

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

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControl = new PlayerControls();
        playerControl.Movement.Enable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        GetInputs();
        Sprint();
        Grounded = isGrounded();
    }

    private void FixedUpdate()
    {
        Movement();
        RbDrag();
        Jump();
        LimitVelocity();
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
            isSprinting = true;
            maxSpeed = runSpeed;
            actualSpeed = Mathf.Lerp(actualSpeed, runSpeed, moveSpeedChange * Time.deltaTime); //Reconcile
        }
        else
        {
            isSprinting = false;
            maxSpeed = walkSpeed;
            actualSpeed = Mathf.Lerp(actualSpeed, walkSpeed, moveSpeedChange * Time.deltaTime); //Reconcile
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

    private IEnumerator jumpCoroutine()
    {
        yield return new WaitForSeconds(jumpCooldown);

        canJump = true;
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
