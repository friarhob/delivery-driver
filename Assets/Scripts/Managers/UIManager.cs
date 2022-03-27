using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] public TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI packagesText;
    [SerializeField] public TextMeshProUGUI timerText;
    
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject gameWonPanel;
    [SerializeField] public GameObject instructionsPanel;

    [SerializeField] public GameObject powerupsPrefab;

    [SerializeField] public GameObject[] packagesPrefabs;
    [SerializeField] public GameObject[] powerupsPrefabs;

    private int initialPackagesQuantity = 3;
    private int initialPowerupsQuantity = 2;

    void Awake()
    {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        EventManager.onGameOver += this.OnGameOver;
        EventManager.onStartNewGame += this.OnStartNewGame;
        EventManager.onGameWon += this.OnGameWon;
    }

    void AddRandomPrefabs(GameObject[] prefabList, string tag, int quantity)
    {
        if(quantity > 0)
        {
            GameObject[] prefabs = GameObject.FindGameObjectsWithTag(tag);

            List<int> indexList = new List<int>();

            // List just the packages that aren't already in the list
            for(int i = 0; i < prefabList.Length; i++)
            {
                bool found = false;

                if(prefabs.Length > 0) //Jump this step if resetting table
                {
                    foreach(GameObject currentPrefab in prefabs)
                    {
                        found = found || (prefabList[i].transform.position == currentPrefab.transform.position);
                    }
                    
                    if(found)
                    {
                        continue;
                    }
                }                

                indexList.Add(i);
            }

            while(quantity > 0)
            {
                int index = UnityEngine.Random.Range(0, indexList.Count - 1);
                GameObject element = prefabList[indexList[index]];
                Instantiate(element);
                indexList.RemoveAt(index);

                quantity--;
            }
        }
    }

    public void AddRandomPackages(int packagesAdded)
    {
        if(packagesAdded > 0)
        {
            AddRandomPrefabs(packagesPrefabs, "Package", packagesAdded);
        }
    }

    public void RemoveRandomPackages(int quantity)
    {
        if(quantity > 0)
        {
            GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");

            while(quantity > 0)
            {
                int index = UnityEngine.Random.Range(0, packages.Length - 1);
                GameObject element = packages[index];
                Destroy(element.gameObject, 0.01f);

                quantity--;
            }
        }
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

        AddRandomPrefabs(packagesPrefabs, "Package", initialPackagesQuantity);
        AddRandomPrefabs(powerupsPrefabs, "PowerUp", initialPowerupsQuantity);
        //Instantiate(powerupsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
        DestroyAllObjectsPerTag("Package");
        DestroyAllObjectsPerTag("PowerUp");
//        DestroyAllObjectsPerTag("RuntimePrefab", 0.1f);
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
