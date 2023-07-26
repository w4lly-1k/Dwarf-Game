using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxEnemyHP;
    [SerializeField] private float viewDistance;
    [SerializeField] private float moveSpeed;

    private Vector2 playerPos;
    private Vector2 playerDirection;
    private bool hasSeen;
    private Transform player;
    private Rigidbody2D rb;

    public float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerDirection.x < 0)
        {
            MoveLeft();
        }
        else if (playerDirection.x > 0)
        {
            MoveRight();
        }

        if (player != null)
        {
            playerPos = player.transform.position;
            playerDirection = (player.transform.position - transform.position).normalized;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hasSeen = true;
            player = collision.GetComponent<Transform>();

        }
    }


    private void MoveLeft() 
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }


    private void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

}
