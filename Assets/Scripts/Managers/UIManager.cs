using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI packagesText;
    [SerializeField] public TextMeshProUGUI timerText;
    
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject car;

    void Start()
    {
        EventManager.onGameOver += this.OnGameOver;
        EventManager.onStartNewGame += this.OnStartNewGame;
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
    }

    void OnStartNewGame()
    {
        gameOverPanel.SetActive(false);
        car.gameObject.transform.position = new Vector3(0f, 0f, 0f);
        car.gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

        // TODO Regenerate packages
    }

    void OnGameOver()
    {
        UpdateTextFields();
        gameOverPanel.SetActive(true);
    }
}
