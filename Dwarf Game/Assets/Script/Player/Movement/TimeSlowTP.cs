using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowTP : MonoBehaviour
{
    private Camera cam;
    private Movement movement;

    private bool canTP;

    [SerializeField] private float timeScaleMod = 0.1f;
    [SerializeField] private float cooldown;

    private void Start()
    {
        cam = Camera.main;
        movement = GetComponent<Movement>();
        canTP = false;
    }

    private void Update()
    {
        Debug.Log(new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y));
        
        if (movement.timeSlowAndTeleport)
        {
            ManageInput();

            if (Input.GetKeyDown(KeyCode.Mouse1) && canTP)
            {
                transform.position = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
                EndSlowTime();
            }
        }
    }

    private void ManageInput()
    {
        if (Input.GetKeyDown(movement.timeSlow))
        {
            StartSlowTime();
        }
        if (Input.GetKeyUp(movement.timeSlow))
        {
            if (canTP)
            {
                EndSlowTime();
            }
        }
    }
    
    private void StartSlowTime()
    {
        Time.timeScale = Time.timeScale * timeScaleMod;
        canTP = true;
    }

    private void EndSlowTime()
    {
        Time.timeScale = Time.timeScale / timeScaleMod;
        canTP = false;
    }
}
