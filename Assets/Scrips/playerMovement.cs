    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float m_speed = 10.0f;
    Rigidbody2D m_Rigidbody;


    public PlayerInput playerinput;

    //store control actions
    private InputAction jumpAction;
    private float jumpTimeCounter;
    private bool canCancelJump = false;

    private bool isJumping = false;
    public float jumpForce = 10f;
    public float jumpHoldForce = 5f;
    public float maxJumpTime = 0.5f;

    public Transform wallCheckPoint;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsWall;
    private bool isTouchingWall;
    private bool isWallSliding;
    public float wallSlideSpeed = 2f;

    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;
    public float wallJumpTime = 0.2f;
    private float wallJumpCounter;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    private bool isGrounded;
    private bool wasGrounded;

    //variables for acceleration and max speed
    public float acceleration = 20f;
    public float deceleration = 30f;
    public float maxSpeed = 10f;
    private float currentSpeed = 0f;

    //animator
    public Animator Animator;

    // Store input for FixedUpdate
    private float xMove;

    //Sound effects
    public AudioClip Jumpfx;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        
        //finding what the jump button is.
        jumpAction = playerinput.actions["jump"];

        //Getting Audiosource
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

        // Input for FixedUpdate
        xMove = Input.GetAxisRaw("Horizontal");

        // Check grounded in physics update
        isJumping = !isGrounded && m_Rigidbody.linearVelocity.y > 0;

        // Ground and wall checks
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        wasGrounded = isGrounded;
        Vector2 wallCheckDir = transform.right * Mathf.Sign(transform.localScale.x);
        isTouchingWall = Physics2D.Raycast(wallCheckPoint.position, wallCheckDir, wallCheckDistance, whatIsWall);


        // Smooth flip based on current speed instead of input
        if (currentSpeed > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (currentSpeed < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // animations
        Animator.SetFloat("Speed", Mathf.Abs(xMove));

        

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        // When you press jump and launch upward
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            jumpTimeCounter = maxJumpTime;
            isJumping = true;
            canCancelJump = true;
            m_Rigidbody.linearVelocity = new Vector2(m_Rigidbody.linearVelocity.x, jumpForce);
            
        }
        // Held jump force
        if (jumpAction.IsPressed() && m_Rigidbody.linearVelocity.y > 0f && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                m_Rigidbody.linearVelocity = new Vector2(m_Rigidbody.linearVelocity.x, jumpHoldForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Cancel jump early on release
        if (jumpAction.WasReleasedThisFrame() && canCancelJump)
        {
            m_Rigidbody.linearVelocity = new Vector2(m_Rigidbody.linearVelocity.x, 0);
            isJumping = false;
            canCancelJump = false;
        }

        //Detect jumping
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            //Jump this frame
            Animator.SetBool("Jumping", true);
            audioSource.clip = Jumpfx;
            audioSource.Play();
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & whatIsGround) != 0)
        {
            // You’ve landed on ground
            Animator.SetBool("Jumping", false);
        }
    }

    void FixedUpdate()
    {
      

        //build the velocity vector
        Vector2 newVelocity = new Vector2(currentSpeed, m_Rigidbody.linearVelocity.y);


        
        // Handle horizontal movement with acceleration/deceleration
        if (xMove != 0 && wallJumpCounter <= 0)
        {
            // Accelerate towards max speed
            currentSpeed += xMove * acceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        }
        else
        {
            // Decelerate to 0 when no input
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration * Time.fixedDeltaTime;
                if (currentSpeed < 0) currentSpeed = 0;
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += deceleration * Time.fixedDeltaTime;
                if (currentSpeed > 0) currentSpeed = 0;
            }
        }


        Collider2D hit = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        isGrounded = hit != null;



       
        // Apply final velocity after all movement and jump logic
        m_Rigidbody.linearVelocity = newVelocity;



    }
}



    
