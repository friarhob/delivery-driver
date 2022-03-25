using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int numberOfPackages;

    void Awake() {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        numberOfPackages = GameObject.FindGameObjectsWithTag("Package").Length;

        EventManager.onPackageDelivered += this.OnPackageDelivered;
    }

    void OnPackageDelivered()
    {
        numberOfPackages--;

        Debug.Log("Current packages: "+numberOfPackages);
    }

}
