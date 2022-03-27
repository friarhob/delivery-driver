using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static int numberOfPackages { get; private set; }
    public static int numberOfLives { get; private set; }
    public static float remainingTime { get; private set; }

    public static bool gameRunning { get; private set; }

    void Awake() {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        EventManager.onPackageDelivered += this.OnPackageDelivered;
        EventManager.onCarCrash += this.OnCarCrash;
        EventManager.onGameOver += this.OnGameOver;
        EventManager.onGameWon += this.OnGameOver;
        EventManager.onStartNewGame += this.NewGame;
    }

    public void AddPackages(int packagesAdded)
    {
        if(packagesAdded < 0)
        {
            UIManager.Instance.RemoveRandomPackages(-packagesAdded);
        }
        else
        {
            UIManager.Instance.AddRandomPackages(packagesAdded);
        }

        numberOfPackages += packagesAdded;
        if(numberOfPackages <= 0)
        {
            numberOfPackages = 0;
            EventManager.gameWon();
        }
    }

    void OnDestroy() {
        EventManager.onPackageDelivered -= this.OnPackageDelivered;
        EventManager.onCarCrash -= this.OnCarCrash;
        EventManager.onGameOver -= this.OnGameOver;
        EventManager.onGameWon -= this.OnGameOver;
        EventManager.onStartNewGame -= this.NewGame;
    }

    void Update()
    {
        if(gameRunning)
        {
            remainingTime -= Time.deltaTime;
            if(remainingTime <= 0)
            {
                EventManager.gameOver();
            }
        }
    }

    public void NewGame()
    {
        numberOfPackages = GameObject.FindGameObjectsWithTag("Package").Length;
        numberOfLives = 5;
        gameRunning = true;
        remainingTime = 60f;
    }

    public void AddTime(float amount)
    {
        if(gameRunning)
        {
            remainingTime += amount;
        }
    }

    void OnPackageDelivered()
    {
        numberOfPackages--;
        if(gameRunning && numberOfPackages <= 0)
        {
            EventManager.gameWon();
        }
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


}
