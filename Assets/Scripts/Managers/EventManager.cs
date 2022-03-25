using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {get; private set;}

    public delegate void CarCrash();
    public static event CarCrash onCarCrash;

    public delegate void PackageDelivered();
    public static event PackageDelivered onPackageDelivered;

    void Awake()
    {
        Instance = Instance ? Instance : this;
    }

    public static void carCrash()
    {
        Debug.Log("Invoking car crash event");
        onCarCrash?.Invoke();
    }

    public static void packageDelivered()
    {
        Debug.Log("Package delivered event");
        onPackageDelivered?.Invoke();
    }
}
