using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    [SerializeField] float steerBaseSpeed = 200f;
    [SerializeField] float moveBaseSpeed = 10f;

    private float steerSpeed;
    private float moveSpeed;

    void Start()
    {
        ResetCarInfo();

        EventManager.onStartNewGame += ResetCarInfo;
        EventManager.onStartNewLevel += ResetCarPosition;
    }

    void Update()
    {
        if (GameManager.Instance.gameRunning)
        {
            float steerAmount = Input.GetAxis("Horizontal") * Time.deltaTime;
            float moveAmount = Input.GetAxis("Vertical") * Time.deltaTime;

            transform.Rotate(0, 0, -steerAmount * steerSpeed);
            transform.Translate(0, moveAmount * moveSpeed, 0);
        }
    }

    void OnDestroy()
    {
        EventManager.onStartNewGame -= ResetCarInfo;
        EventManager.onStartNewLevel -= ResetCarPosition;
    }

    void ResetCarPosition()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.identity;
    }

    void ResetCarInfo()
    {
        ResetCarPosition();

        steerSpeed = steerBaseSpeed;
        moveSpeed = moveBaseSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EventManager.Instance.carCrash();
    }

    public void MultiplySpeed(float factor)
    {
        moveSpeed *= factor;
    }
}