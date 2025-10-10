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


    private bool isJumping = false;
    private float jumpTimeCounter;
    public float jumpForce = 10f;
    public float jumpHoldForce = 5f;
    public float maxJumpTime = 0.5f;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    private bool isGrounded;

    //variables for acceleration and max speed
    public float acceleration = 20f;
    public float deceleration = 30f;
    public float maxSpeed = 10f;
    private float currentSpeed = 0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        //finding what the jump button is.
        jumpAction = playerinput.actions["jump"];
    }


    // Update is called once per frame
    void Update()
    {   //Moving left and right.
        float xMove = Input.GetAxisRaw("Horizontal");

        if (xMove != 0)
        {
            // Accelerate towards max speed
            currentSpeed += xMove * acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        }
        else
        {
            // Decelerate to 0 when no input
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration * Time.deltaTime;
                if (currentSpeed < 0) currentSpeed = 0;
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += deceleration * Time.deltaTime;
                if (currentSpeed > 0) currentSpeed = 0;
            }
        }

        m_Rigidbody.linearVelocity = new Vector2(currentSpeed, m_Rigidbody.linearVelocity.y);


        m_Rigidbody.linearVelocity = new Vector2(xMove * m_speed, m_Rigidbody.linearVelocity.y);

        //Code for checking if you are grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        
        //Adding force upwards if you are grounded
        if (jumpAction.WasPressedThisFrame() && isGrounded == true)
        {
            jumpTimeCounter = maxJumpTime;
            m_Rigidbody.linearVelocity = new Vector2(m_Rigidbody.linearVelocity.x, jumpForce);
        }
        // held jump force
        if (jumpAction.IsPressed() && m_Rigidbody.linearVelocity.y > 0f)
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
        // Code to stop jumping if the key was released early
        if (jumpAction.WasReleasedThisFrame())
        {
            m_Rigidbody.linearVelocity = new Vector3(m_Rigidbody.linearVelocity.x, 0, m_Rigidbody.linearVelocity.y);
            isJumping = false ;
        }


        

    }

    void FixedUpdate()
    {
        // Code for checking if you are grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        // Code for checking if you are jumping
        isJumping = !isGrounded && m_Rigidbody.linearVelocity.y > 0;

        m_Rigidbody.linearVelocity = new Vector2(currentSpeed, m_Rigidbody.linearVelocity.y);

    }








}
