using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI packagesText;
    void Update()
    {
        if(GameManager.gameRunning)
        {
            livesText.text = "Lives: "+GameManager.numberOfLives;
            packagesText.text = "Packages: "+GameManager.numberOfPackages;  
        }
    }
}
