using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private float moveSpeed = 3;

    private float viewDistance = 5;
    private Vector2 playerPos;
    private Vector2 playerDirection;
    private bool hasSeen;
    private Transform player;
    private Player playerData;
    private Rigidbody2D rb;

    public float damage;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerData = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y).magnitude <= viewDistance)
        {
            hasSeen = true;
        }

        if (hasSeen)
        {
            if (playerDirection.x < 0)
            {
                MoveLeft();
            }
            else if (playerDirection.x > 0)
            {
                MoveRight();
            }
        }

        if (player != null)
        {
            playerPos = player.transform.position;
            playerDirection = (player.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerData.TakeDamage(damage);
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
