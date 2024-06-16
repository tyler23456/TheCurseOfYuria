using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargeterDatabase : MonoBehaviour
{
    public static TargeterDatabase Instance { get; private set; }

    [SerializeField] TargeterBase nearbyAllieTargeter;

    public TargeterBase getNearbyAllieTargeter => nearbyAllieTargeter;

    void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        
    }
}
