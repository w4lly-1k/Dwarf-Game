using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GroundEnemy enemy;
    private ScaleCloak scaleCloak;

    [SerializeField] private float maxHealth;

    private float currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        scaleCloak = GetComponent<ScaleCloak>();
    }

    void Update()
    {
        Die();
    }

    public void TakeDamage(float damage)
    {
        if (!scaleCloak.invulnerable)
        {
            currentHealth -= damage;
        }
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
