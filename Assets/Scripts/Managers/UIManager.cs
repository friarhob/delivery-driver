using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI packagesText;
    [SerializeField] public TextMeshProUGUI timerText;
    
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject gameWonPanel;
    [SerializeField] public GameObject car;

    [SerializeField] public GameObject packagesPrefab;

    // TODO reset powerups

    void Start()
    {
        EventManager.onGameOver += this.OnGameOver;
        EventManager.onStartNewGame += this.OnStartNewGame;
        EventManager.onGameWon += this.OnGameWon;
    }

    void Update()
    {
        if(GameManager.gameRunning)
        {
            UpdateTextFields();
        }
    }

    void UpdateTextFields()
    {
        livesText.text = "Lives: "+GameManager.numberOfLives;
        packagesText.text = "Packages: "+GameManager.numberOfPackages;
        timerText.text = ""+Mathf.CeilToInt(GameManager.remainingTime);
    }

    void OnDestroy()
    {
        EventManager.onGameOver -= this.OnGameOver;
        EventManager.onStartNewGame -= this.OnStartNewGame;
        EventManager.onGameWon -= this.OnGameWon;
    }

    void OnStartNewGame()
    {
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        car.gameObject.transform.position = new Vector3(0f, 0f, 0f);
        car.gameObject.transform.rotation = Quaternion.identity;

        Instantiate(packagesPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void OnGameOver()
    {
        UpdateTextFields();
        gameOverPanel.SetActive(true);

        // Destroy all packages current in game
        GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");
        if(packages.Length > 0)
        {
            foreach(GameObject package in packages)
            {
                Debug.Log(package);
                Destroy(package, 0.1f);
            }
        }
    }
    void OnGameWon()
    {
        UpdateTextFields();
        gameWonPanel.SetActive(true);
    }


}
