using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }
}
