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
    [SerializeField] public GameObject instructionsPanel;

    [SerializeField] public GameObject powerupsPrefab;

    [SerializeField] public GameObject[] packagesPrefabs;

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
        CloseAllPanels();

        RegeneratePackages();
        Instantiate(powerupsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void RegeneratePackages()
    {
        DestroyAllObjectsPerTag("Package");

        List<int> indexList = new List<int>();

        for(int i = 0; i < packagesPrefabs.Length; i++)
        {
            indexList.Add(i);
        }

        for(int i = 0; i < 3; i++)
        {
            int index = UnityEngine.Random.Range(0, indexList.Count - 1);
            GameObject element = packagesPrefabs[indexList[index]];
            Instantiate(element);
        }
    }

    private void DestroyAllObjectsPerTag(string tag, float delay = 0.01f)
    {
        GameObject[] elements = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject element in elements)
        {
            Destroy(element, delay);
        }
    }

    void RemovePrefabs()
    {
        DestroyAllObjectsPerTag("RuntimePrefab", 0.1f);
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

    void CloseAllPanels()
    {
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        instructionsPanel.SetActive(false);
    }

    public void openInstructions()
    {
        CloseAllPanels();

        instructionsPanel.SetActive(true);
    }


}
