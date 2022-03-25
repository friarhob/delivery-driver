using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int numberOfPackages;

    void Awake()
    {
        Instance = Instance ? Instance : this;
    }

}
