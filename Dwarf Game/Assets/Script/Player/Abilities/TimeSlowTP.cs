using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowTP : MonoBehaviour
{
    private Camera cam;
    private Movement movement;

    private bool timeSlowed;
    private bool offCooldown;

    [SerializeField] private float timeScaleMod = 0.1f;
    [SerializeField] private float cooldown;
    [SerializeField] private float maxDuration;

    private float elapsedTime;
    private float tpTime;

    private void Start()
    {
        cam = Camera.main;
        movement = GetComponent<Movement>();
        timeSlowed = false;
        elapsedTime = cooldown;
    }

    private void Update()
    {
        Teleport();
    }

    private void ManageInput()
    {
        if (offCooldown)
        {
            if (Input.GetKeyDown(movement.timeSlow))
            {
                StartSlowTime();
            }
            if (Input.GetKeyUp(movement.timeSlow))
            {
                if (timeSlowed)
                {
                    EndSlowTime();
                }
            }
        }
    }
    
    private void StartSlowTime()
    {
        Time.timeScale *= timeScaleMod;
        timeSlowed = true;

        StartCoroutine(EndAtMaxTime());
    }

    private void EndSlowTime()
    {
        Time.timeScale /= timeScaleMod;
        timeSlowed = false;

        tpTime = Time.time;
        elapsedTime = 0;
    }

    IEnumerator EndAtMaxTime()
    {
        yield return new WaitForSecondsRealtime(maxDuration);
        if (timeSlowed)
        {
            EndSlowTime();
        }
    }



    private void Teleport()
    {
        if (movement.timeSlowAndTeleport)
        {
            ManageInput();

            if (Input.GetKeyDown(movement.teleport) && timeSlowed)
            {
                transform.position = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
                EndSlowTime();
            }
        }

        if (elapsedTime >= cooldown)
        {
            offCooldown = true;
            elapsedTime = cooldown;
        }
        else
        {
            offCooldown = false;
        }

        elapsedTime = Time.time - tpTime;
    }
}
