using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int numberOfPackages { get; private set; }
    public static int numberOfLives { get; private set; }

    public static bool gameRunning;

    void Awake() {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        numberOfPackages = GameObject.FindGameObjectsWithTag("Package").Length;
        numberOfLives = 5;
        gameRunning = true;

        EventManager.onPackageDelivered += this.OnPackageDelivered;
        EventManager.onCarCrash += this.OnCarCrash;
        EventManager.onGameOver += this.OnGameOver;
    }

    void OnPackageDelivered()
    {
        numberOfPackages--;
    }

    void OnCarCrash()
    {
        numberOfLives--;
        if(numberOfLives <= 0)
        {
            EventManager.gameOver();
        }
    }

    void OnGameOver()
    {
        gameRunning = false;
    }

    void OnDestroy() {
        EventManager.onPackageDelivered -= this.OnPackageDelivered;
        EventManager.onCarCrash -= this.OnCarCrash;
        EventManager.onGameOver -= this.OnGameOver;
    }

}
