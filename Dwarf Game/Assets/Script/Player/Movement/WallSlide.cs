using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    private Movement movement;

    private LayerMask slidable;

    private bool wallLeft;
    private bool wallRight;

    private bool sliding;

    private float wallJumpForce = 2;
    
    void Start()
    {
        movement = GetComponent<Movement>();
        
        wallLeft = false;
        wallRight = false;
    }

    void Update()
    {
        if (movement.wallSlide)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, 0.1f, slidable))
            {
                wallLeft = true;
            }
            else if (Physics2D.Raycast(transform.position, Vector2.right, 0.1f, slidable))
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

            if (sliding)
            {
                if (Input.GetKeyDown(movement.jump))
                {
                    if (wallLeft)
                    {
                        movement.rb.AddForce(Vector2.up + Vector2.right * wallJumpForce, ForceMode2D.Impulse);
                    }
                    else if (wallRight)
                    {
                        movement.rb.AddForce(Vector2.up + Vector2.left * wallJumpForce, ForceMode2D.Impulse);
                    }
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
}
