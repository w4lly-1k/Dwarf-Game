using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcasting : MonoBehaviour
{
    private KeyBinds keybinds;
    private Camera cam;
    private Movement movement;
    private Color originalColor;
    private SpriteRenderer square;
    private Rigidbody2D rb;

    [Header("References")]
    public GameObject firebolt;
    
    enum Spell1 {Firebolt};
    enum Spell2 {SuperJump};
    enum Spell3 {TimeSlow};

    [Header("Spells")]
    [SerializeField] private Spell1 spell1;
    [SerializeField] private Spell2 spell2;
    [SerializeField] private Spell3 spell3;

    private float spell1Cooldown = 5;
    private float spell2Cooldown = 5;
    private float spell3Cooldown = 10;

    private float elapsedTime1;
    private float elapsedTime2;
    private float elapsedTime3;

    private float spell1Time;
    private float spell2Time;
    private float spell3Time;

    private void Start()
    {
        keybinds = GetComponent<KeyBinds>();
        square = GetComponentInChildren<SpriteRenderer>();
        movement = GetComponent<Movement>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        originalColor = square.color;
    }

    void Update()
    {
        if(Input.GetKey(keybinds.spellMode))
        {
            square.color = Color.magenta;
            
            if (Input.GetKeyDown(keybinds.spell1))
            {
                if (elapsedTime1 >= spell1Cooldown)
                {
                    spell1Time = Time.time;

                    if (spell1 == Spell1.Firebolt)
                    {
                        FireBolt();
                    }
                }
            }

            if (Input.GetKeyDown(keybinds.spell2))
            {
                if (elapsedTime2 >= spell2Cooldown)
                {
                    spell2Time = Time.time;

                    if (spell2 == Spell2.SuperJump)
                    {
                        SuperJump();
                    }
                }
            }

            if (Input.GetKeyDown(keybinds.spell3))
            {
                if (elapsedTime3 >= spell3Cooldown)
                {
                    spell3Time = Time.time;
                    
                    if (spell3 == Spell3.TimeSlow)
                    {
                        TimeSlow();
                    }
                }
            }
        }
        else
        {
            square.color = originalColor;
        }

        elapsedTime1 = Time.time - spell1Time;
        elapsedTime2 = Time.time - spell2Time;
        elapsedTime3 = Time.time - spell3Time;
    }

    private void FireBolt()
    {
        GameObject thisFirebolt = Instantiate(firebolt, transform.position, transform.rotation);

        Vector2 mousePos = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
        Rigidbody2D rb = thisFirebolt.GetComponent<Rigidbody2D>();
        float shotForce = 10;

        if (mousePos.x - transform.position.x < 0)
        {
            rb.AddForce(Vector2.left * shotForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.right * shotForce, ForceMode2D.Impulse);
        }
    }

    private void SuperJump()
    {
        rb.AddForce(transform.up * 15, ForceMode2D.Impulse);
    }

    private void TimeSlow()
    {
        Time.timeScale *= 0.1f;
        movement.moveSpeed *= 10;
        StartCoroutine(TimeSlowDuration());
    }
    IEnumerator TimeSlowDuration()
    {
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale /= 0.1f;
        movement.moveSpeed /= 10;
    }
}
