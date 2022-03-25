using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {get; private set;}

    public delegate void CarCrash();
    public event CarCrash onCarCrash;

    void Awake()
    {
        Instance = Instance ? Instance : this;
    }

    public void carCrash()
    {
        Debug.Log("Invoking car crash event");
        onCarCrash?.Invoke();
    }
}
