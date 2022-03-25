using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    [SerializeField] float steerBaseSpeed = 200f;
    [SerializeField] float moveBaseSpeed = 10f;
    [SerializeField] float boostSpeedMultiplier = 1.5f;
    [SerializeField] float bumpSpeedMultiplier = 1/1.5f;
    [SerializeField] float delayUntilDestroySpeedMultiplier = 0.2f;


    void Update()
    {
        if(GameManager.gameRunning)
        {
            float steerAmount = Input.GetAxis("Horizontal")*Time.deltaTime;
            float moveAmount = Input.GetAxis("Vertical")*Time.deltaTime;

            transform.Rotate(0, 0, -steerAmount*steerBaseSpeed);
            transform.Translate(0, moveAmount*moveBaseSpeed, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EventManager.carCrash();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "SpeedBoost")
        {
            moveBaseSpeed *= boostSpeedMultiplier;

            Destroy(other.gameObject, delayUntilDestroySpeedMultiplier);
        }

        if(other.tag == "SpeedBump")
        {
            moveBaseSpeed *= bumpSpeedMultiplier;

            Destroy(other.gameObject, delayUntilDestroySpeedMultiplier);
        }
    }
}
