using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private WallSlide wallSlideCS;
    private KeyBinds keybinds;
    
    [Header("Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Layers")]
    [SerializeField] private LayerMask groundIgnore;

    [Header("Abilities")]
    public bool doubleJump;
    public bool wallSlide;
    public bool timeSlowAndTeleport;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool hasJumped;

    [HideInInspector] public Rigidbody2D rb;
    private Transform groundCheck;

    private void Start()
    {
        keybinds = GetComponent<KeyBinds>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        wallSlideCS = GetComponent<WallSlide>();
    }

    private void Update()
    {
        GroundCheck();        
        MovePlayer();
        Jump();
    }

    private void MovePlayer()
    {
        if (!wallSlideCS.slideJumping)
        {
            if (Input.GetKey(keybinds.left))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
            if (Input.GetKey(keybinds.right))
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            if (Input.GetKeyUp(keybinds.right))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if (Input.GetKeyUp(keybinds.left))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    private void Jump()
    {
        if (doubleJump)
        {
            if (Input.GetKeyDown(keybinds.jump) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown(keybinds.jump) && !isGrounded && !hasJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                hasJumped = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(keybinds.jump) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (isGrounded)
        {
            hasJumped = false;
        }
    }


    private void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, ~groundIgnore);
    }
}
