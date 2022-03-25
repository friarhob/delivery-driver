using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {get; private set;}

    public delegate void CarCrash();
    public static event CarCrash onCarCrash;
    private static System.DateTime lastCarCrash;

    public delegate void PackageDelivered();
    public static event PackageDelivered onPackageDelivered;

    void Awake()
    {
        Instance = Instance ? Instance : this;
    }

    void Start()
    {
        lastCarCrash = System.DateTime.Now;    
    }

    public static void carCrash()
    {
        // Make sure multiple crashes don't occur too close to each other
        System.DateTime now = System.DateTime.Now;
        System.TimeSpan timeSpan = now - lastCarCrash;
        
        lastCarCrash = System.DateTime.Now;

        if(timeSpan.TotalSeconds > 0.5)
            onCarCrash?.Invoke();
    }

    public static void packageDelivered()
    {
        Debug.Log("Package delivered event");
        onPackageDelivered?.Invoke();
    }
}
