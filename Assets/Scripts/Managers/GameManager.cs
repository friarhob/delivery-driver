using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int numberOfPackages { get; private set; }
    public static int numberOfLives { get; private set; }

    void Awake() {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        numberOfPackages = GameObject.FindGameObjectsWithTag("Package").Length;
        numberOfLives = 5;

        EventManager.onPackageDelivered += this.OnPackageDelivered;
        EventManager.onCarCrash += this.OnCarCrash;
    }

    void OnPackageDelivered()
    {
        numberOfPackages--;
    }

    void OnCarCrash()
    {
        numberOfLives--;
    }

    void OnDestroy() {
        EventManager.onPackageDelivered -= this.OnPackageDelivered;
        EventManager.onCarCrash -= this.OnCarCrash;
    }

}
