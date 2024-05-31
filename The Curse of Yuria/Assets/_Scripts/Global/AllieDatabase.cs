using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllieDatabase : MonoBehaviour
{
    public static AllieDatabase Instance { get; private set; }

    [SerializeField] GameObject river;
    [SerializeField] GameObject sarah;
    [SerializeField] GameObject nate;

    Dictionary<string, GameObject> alliePrefabs;

    void Awake()
    {
        Instance = this;

        alliePrefabs.Add(river.name, river);
        alliePrefabs.Add(sarah.name, sarah);
        alliePrefabs.Add(nate.name, nate);
    }

    public IActor Instantiate(string allieName)
    {
        return Instantiate(alliePrefabs[allieName]).GetComponent<IActor>();
    }
}
