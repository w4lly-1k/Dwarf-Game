using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    //DON'T TOUCH, IT WORKS
    
    private Movement movement;

    private Transform leftWallCheck;
    private Transform rightWallCheck;

    [SerializeField] private Vector2 wallJumpDirection;

    [SerializeField] private float slideGrav = 0.3f;
    [SerializeField] private LayerMask wallLayer;

    private bool wallLeft;
    private bool wallRight;

    private bool slidingOnLeft;
    private bool slidingOnRight;
    private bool sliding;
    [HideInInspector] public bool slideJumping;

    private bool hasStarted;

    [SerializeField] private float wallJumpForce = 2;
    
    void Start()
    {
        movement = GetComponent<Movement>();
        leftWallCheck = GameObject.Find("LeftWallCheck").GetComponent<Transform>();
        rightWallCheck = GameObject.Find("RightWallCheck").GetComponent<Transform>();
    }

    void Update()
    {
        DetectWall();
        ManageWallSlide();
        SlideJump();
        ManageBools();
    }

    void DetectWall()
    {
        if (Physics2D.Raycast(leftWallCheck.position, Vector2.left, 0.1f, wallLayer))
        {
            wallLeft = true;
        }
        else if (Physics2D.Raycast(rightWallCheck.position, Vector2.right, 0.1f, wallLayer))
        {
            wallRight = true;
        }
        else
        {
            wallLeft = false;
            wallRight = false;
        }
    }
    void ManageWallSlide()
    {
        if (!movement.isGrounded)
        {
            if (wallLeft && Input.GetKey(movement.left))
            {
                StartSlide(false);
            }
            else if (wallRight && Input.GetKey(movement.right))
            {
                StartSlide(true);
            }
            else
            {
                EndSlide();
            }
        }
        else
        {
            EndSlide();
        }
    }
    void StartSlide(bool right)
    {
        if (!hasStarted)
        {
            movement.rb.velocity = new Vector2(movement.rb.velocity.x, 0);
            hasStarted = true;
        }

        sliding = true;
        slideJumping = true;
        movement.rb.gravityScale = slideGrav;
        
        if (right)
        {
            slidingOnRight = true;
        }
        else
        {
            slidingOnLeft = true;
        }
    }
    void EndSlide()
    {
        movement.rb.gravityScale = 1;
        
        slidingOnRight = false;
        slidingOnLeft = false;
        sliding = false;
        hasStarted = false;
    }
    void SlideJump()
    {
        if (sliding)
        {
            if (Input.GetKeyDown(movement.jump))
            {
                slideJumping = true;
                movement.hasJumped = false;

                if (slidingOnLeft)
                {
                    movement.rb.AddForce(new Vector2(wallJumpDirection.x, wallJumpDirection.y) * wallJumpForce, ForceMode2D.Impulse);
                }
                else if (slidingOnRight)
                {
                    movement.rb.AddForce(new Vector2(-wallJumpDirection.x, wallJumpDirection.y) * wallJumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }
    void ManageBools()
    {
        if (movement.isGrounded)
        {
            slideJumping = false;
        }
        if (sliding)
        {
            movement.hasJumped = true;
        }
        if (movement.isGrounded && !Input.GetKey(movement.right) && !Input.GetKey(movement.left))
        {
            movement.rb.velocity = new Vector2(0, movement.rb.velocity.y);
        }
    }
}
