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
    [SerializeField] public TextMeshProUGUI scoreText;

    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject levelWonPanel;
    [SerializeField] public GameObject instructionsPanel;

    [SerializeField] public GameObject powerupsPrefab;

    [SerializeField] public GameObject[] packagesPrefabs;
    [SerializeField] public GameObject[] powerupsPrefabs;

    void Awake()
    {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        EventManager.onGameOver += this.OnGameOver;
        EventManager.onStartNewGame += this.OnStartNewLevel;
        EventManager.onFinishLevel += this.OnFinishLevel;
        EventManager.onStartNewLevel += this.OnStartNewLevel;
    }

    void Update()
    {
        if (GameManager.Instance.gameRunning)
        {
            UpdateTextFields();
        }
    }

    void OnDestroy()
    {
        EventManager.onGameOver -= this.OnGameOver;
        EventManager.onStartNewGame -= this.OnStartNewLevel;
        EventManager.onFinishLevel -= this.OnFinishLevel;
        EventManager.onStartNewLevel -= this.OnStartNewLevel;
    }

    void AddRandomPrefabs(GameObject[] prefabList, string tag, int quantity)
    {
        if (quantity > 0)
        {
            GameObject[] prefabs = GameObject.FindGameObjectsWithTag(tag);

            List<int> indexList = new List<int>();

            // List just the packages that aren't already in the list
            for (int i = 0; i < prefabList.Length; i++)
            {
                bool found = false;

                if (prefabs.Length > 0) //Jump this step if resetting table
                {
                    foreach (GameObject currentPrefab in prefabs)
                    {
                        found = found || (prefabList[i].transform.position == currentPrefab.transform.position);
                    }

                    if (found)
                    {
                        continue;
                    }
                }

                indexList.Add(i);
            }

            while (indexList.Count > 0 && quantity > 0)
            {
                int index = UnityEngine.Random.Range(0, indexList.Count);
                GameObject element = prefabList[indexList[index]];
                Instantiate(element);
                indexList.RemoveAt(index);

                quantity--;
            }
        }
    }

    public void AddRandomPackages(int packagesAdded)
    {
        if (packagesAdded > 0)
        {
            AddRandomPrefabs(packagesPrefabs, "Package", packagesAdded);
        }
    }

    public void RemoveRandomPackages(int quantity)
    {
        while (quantity > 0)
        {
            GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");

            if (packages.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, packages.Length);
                GameObject element = packages[index];
                Destroy(element.gameObject, 0.01f);
                EventManager.Instance.packageDelivered();
            }

            quantity--;
        }
    }

    void UpdateTextFields()
    {
        livesText.text = "Lives: " + GameManager.Instance.numberOfLives;
        packagesText.text = "Packages: " + GameManager.Instance.numberOfPackages;
        timerText.text = "" + Mathf.CeilToInt(GameManager.Instance.remainingTime);
        scoreText.text = "Score: " + GameManager.Instance.score;
    }

    void OnStartNewLevel()
    {
        int initialPackagesQuantity = GameManager.Instance.level;
        int initialPowerupsQuantity = Mathf.CeilToInt(GameManager.Instance.level / 2.0f);

        CloseAllPanels();

        AddRandomPrefabs(packagesPrefabs, "Package", initialPackagesQuantity);
        AddRandomPrefabs(powerupsPrefabs, "PowerUp", initialPowerupsQuantity);
    }

    void DestroyAllObjectsPerTag(string tag, float delay = 0.01f)
    {
        GameObject[] elements = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject element in elements)
        {
            Destroy(element, delay);
        }
    }

    void RemovePrefabs()
    {
        DestroyAllObjectsPerTag("Package");
        DestroyAllObjectsPerTag("PowerUp");
    }

    void OnGameOver()
    {
        UpdateTextFields();
        gameOverPanel.SetActive(true);

        RemovePrefabs();
    }
    void OnFinishLevel()
    {
        UpdateTextFields();
        levelWonPanel.SetActive(true);
    }

    void CloseAllPanels()
    {
        gameOverPanel.SetActive(false);
        levelWonPanel.SetActive(false);
        instructionsPanel.SetActive(false);
    }

    public void openInstructions()
    {
        CloseAllPanels();

        instructionsPanel.SetActive(true);
    }


}
