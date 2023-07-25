using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    private Movement movement;

    private Transform leftWallCheck;
    private Transform rightWallCheck;

    [SerializeField] private LayerMask wall;

    private bool wallLeft;
    private bool wallRight;

    private bool sliding;

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

        if (sliding)
        {
            if (Input.GetKeyDown(movement.jump))
            {
                if (wallLeft)
                {
                    movement.rb.AddForce(Vector2.up + Vector2.right * wallJumpForce, ForceMode2D.Impulse);
                    EndWallSLide();
                }
                else if (wallRight)
                {
                    movement.rb.AddForce(Vector2.up + Vector2.left * wallJumpForce, ForceMode2D.Impulse);
                    EndWallSLide();
                }
            }
        }
    }
    
    void StartWallSlide()
    {
        movement.rb.gravityScale = 0.3f;
        sliding = true;
    }

    void EndWallSLide()
    {
        movement.rb.gravityScale = 1;
        sliding = false;
    }

    void DetectWall()
    {
        if (Physics2D.Raycast(leftWallCheck.position, Vector2.left, 0.1f, wall))
        {
            wallLeft = true;
        }
        else if (Physics2D.Raycast(rightWallCheck.position, Vector2.right, 0.1f, wall))
        {
            wallRight = true;
        }

        if (wallLeft || wallRight)
        {
            if (!movement.isGrounded)
            {
                StartWallSlide();
            }
            else
            {
                EndWallSLide();
            }
        }
        else
        {
            EndWallSLide();
        }
    }
}
