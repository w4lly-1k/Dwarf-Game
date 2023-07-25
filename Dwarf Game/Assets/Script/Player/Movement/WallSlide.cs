using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    private Movement movement;

    private Transform leftWallCheck;
    private Transform rightWallCheck;

    [SerializeField] private float slideGrav = 0.3f;
    [SerializeField] private LayerMask wallLayer;

    private bool wallLeft;
    private bool wallRight;

    private bool slidingOnLeft;
    private bool slidingOnRight;

    private bool hasStarted;

    private float wallJumpForce = 2;
    
    void Start()
    {
        movement = GetComponent<Movement>();
        leftWallCheck = GameObject.Find("LeftWallCheck").GetComponent<Transform>();
        rightWallCheck = GameObject.Find("RightWallCheck").GetComponent<Transform>();
    }

    void Update()
    {
        DetectWall();
        ManageWallRun();
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
    void ManageWallRun()
    {
        if (!movement.isGrounded)
        {
            if (wallLeft)
            {
                StartSlide(false);
            }
            else if (wallRight)
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
        hasStarted = false;
    }
}
