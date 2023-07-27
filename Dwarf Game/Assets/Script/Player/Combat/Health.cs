using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    private GroundEnemy enemy;

    [SerializeField] private float maxHealth;
    private float currentHealth;
    

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Die();
        Debug.Log(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = collision.GetComponentInParent<GroundEnemy>();

            if (enemy != null)
            {
                TakeDamage(enemy.damage);
            }
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }
}
