using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int numberOfPackages { get; private set; }
    public int numberOfLives { get; private set; }
    public float remainingTime { get; private set; }

    public bool gameRunning { get; private set; }
    public int level { get; private set; }

    void Awake() {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        EventManager.onCarCrash += this.CarCrash;
        EventManager.onGameOver += this.GameOver;
        EventManager.onFinishLevel += this.FinishLevel;
        EventManager.onStartNewLevel += this.StartNextLevel;
        EventManager.onStartNewGame += this.NewGame;
        level = 1;
        gameRunning = false;
    }

    private void FinishLevel()
    {
        level++;
        gameRunning = false;
    }

    void StartNextLevel()
    {
        gameRunning = true;
        remainingTime = 60f;
    }

    public void AddPackages(int packagesAdded)
    {
        if(packagesAdded < 0)
        {
            UIManager.Instance.RemoveRandomPackages(-packagesAdded);
        }
        else if(packagesAdded > 0)
        {
            UIManager.Instance.AddRandomPackages(packagesAdded);
            EventManager.packageDelivered();
        }
    }


    void OnDestroy() {
        EventManager.onCarCrash -= this.CarCrash;
        EventManager.onGameOver -= this.GameOver;
        EventManager.onFinishLevel -= this.StartNextLevel;
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

            bool carCarrying = GameObject.FindGameObjectWithTag("Car").GetComponent<CarDelivery>().hasPackage;
            numberOfPackages = GameObject.FindGameObjectsWithTag("Package").Length + (carCarrying ? 1 : 0);
            if(numberOfPackages == 0)
            {
                EventManager.finishLevel();
            }
            
        }
    }

    public void NewGame()
    {
        numberOfLives = 5;
        StartNextLevel();
    }

    public void AddTime(float amount)
    {
        if(gameRunning)
        {
            remainingTime += amount;
        }
    }

    void CarCrash()
    {
        numberOfLives--;
        if(numberOfLives <= 0)
        {
            EventManager.gameOver();
        }
    }

    void GameOver()
    {
        level=1;
        gameRunning = false;
    }


}
