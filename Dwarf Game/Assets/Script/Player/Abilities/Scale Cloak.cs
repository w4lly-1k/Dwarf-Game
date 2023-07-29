using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCloak : MonoBehaviour
{
    private KeyBinds keybinds;
    private Player player;
    private Movement movement;
    private SpriteRenderer playerSquare;
    
    [HideInInspector] public bool invulnerable;
    private float maxDuration = 4f;

    private float scaleCloakTime;
    [SerializeField] private float cooldown;
    private float elapsedTime;

    private Color originalColor;
    
    void Start()
    {
        keybinds = GetComponent<KeyBinds>();
        player = GetComponent<Player>();
        playerSquare = GetComponentInChildren<SpriteRenderer>();
        movement = GetComponent<Movement>();
        
        invulnerable = false;
        originalColor = playerSquare.color;
    }

    void Update()
    {
        elapsedTime = Time.time - scaleCloakTime;
        
        if (movement.scaleCloak)
        {
            GetInput();
        }
    }

    private void BeginScaleCloak()
    {
        invulnerable = true;
        movement.canMove = false;
        playerSquare.color = Color.blue;
        StartCoroutine(EndAfterDuration());
    }

    private void EndScaleCloak()
    {
        invulnerable = false;
        movement.canMove = true;
        playerSquare.color = originalColor;

        scaleCloakTime = Time.time;
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(keybinds.scaleCloak))
        {
            if (elapsedTime >= cooldown)
            {
                BeginScaleCloak();
            }
        }
        if(Input.GetKeyUp(keybinds.scaleCloak))
        {
            if (invulnerable)
            {
                EndScaleCloak();
            }
        }
    }

    IEnumerator EndAfterDuration()
    {
        yield return new WaitForSeconds(maxDuration);
        if (invulnerable)
        {
            EndScaleCloak();
        }
    }
}