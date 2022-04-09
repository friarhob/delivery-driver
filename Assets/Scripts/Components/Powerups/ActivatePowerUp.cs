using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePowerUp : MonoBehaviour
{
    [SerializeField] public float speedMultiplier = 1f;
    [SerializeField] public int packagesAdded = 0;
    [SerializeField] public float timeAdded = 0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Car")
        {
            CarDriver car = other.gameObject.GetComponent<CarDriver>();

            car.MultiplySpeed(speedMultiplier);
            GameManager.Instance.AddTime(timeAdded);
            GameManager.Instance.AddPackages(packagesAdded);

            Destroy(this.gameObject, 0.01f);
        }
    }



}
