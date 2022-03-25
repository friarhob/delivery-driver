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
    [SerializeField] public GameObject powerupsPrefab;

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

        // TODO refactor this to CarDriver 
        car.gameObject.transform.position = new Vector3(0f, 0f, 0f);
        car.gameObject.transform.rotation = Quaternion.identity;

        Instantiate(packagesPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(powerupsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void RemovePrefabs()
    {
        GameObject[] elements = GameObject.FindGameObjectsWithTag("RuntimePrefab");
        foreach(GameObject element in elements)
        {
            Destroy(element, 0.1f);
        }
    }

    void OnGameOver()
    {
        UpdateTextFields();
        gameOverPanel.SetActive(true);

        RemovePrefabs();

    }
    void OnGameWon()
    {
        UpdateTextFields();
        gameWonPanel.SetActive(true);
    }


}
