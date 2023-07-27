using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private WallSlide wallSlideCS;
    
    [Header("Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Keybinds")]
    [SerializeField] public KeyCode left = KeyCode.A;
    [SerializeField] public KeyCode right = KeyCode.D;
    [SerializeField] public KeyCode jump = KeyCode.Space;
    [SerializeField] public KeyCode timeSlow = KeyCode.LeftShift;
    [SerializeField] public KeyCode TP = KeyCode.Mouse1;

    [Header("Layers")]
    [SerializeField] private LayerMask playerLayer;

    [Header("Abilities")]
    [SerializeField] public bool doubleJump;
    [SerializeField] public bool wallSlide;
    [SerializeField] public bool timeSlowAndTeleport;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool hasJumped;

    [HideInInspector] public Rigidbody2D rb;
    private Transform groundCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        wallSlideCS = GetComponent<WallSlide>();
    }

    private void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, ~playerLayer);
        
        MovePlayer();
        Jump();
    }

    private void MovePlayer()
    {
        if (!wallSlideCS.slideJumping)
        {
            if (Input.GetKey(left))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
            if (Input.GetKey(right))
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            if (Input.GetKeyUp(right))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (Input.GetKeyUp(left))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    private void Jump()
    {
        if (doubleJump)
        {
            if (Input.GetKeyDown(jump) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown(jump) && !isGrounded && !hasJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                hasJumped = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(jump) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (isGrounded)
        {
            hasJumped = false;
        }
    }
}
