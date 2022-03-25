using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

     public static EventManager eventManager;

    void Awake() {
        Instance = Instance ? Instance : this;
    }    

    void Start()
    {
        eventManager = EventManager.Instance;
    }

}
