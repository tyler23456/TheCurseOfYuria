using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllieDatabase : MonoBehaviour
{
    public static AllieDatabase Instance { get; private set; }

    [SerializeField] GameObject river;
    [SerializeField] GameObject sarah;
    [SerializeField] GameObject nate;
    [SerializeField] GameObject juel;

    Dictionary<string, GameObject> alliePrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        Instance = this;

        alliePrefabs.Add(river.name, river);
        alliePrefabs.Add(sarah.name, sarah);
        alliePrefabs.Add(nate.name, nate);
        alliePrefabs.Add(juel.name, juel);
    }

    public IActor Instantiate(string allieName)
    {
        return Instantiate(alliePrefabs[allieName]).GetComponent<IActor>();
    }
}
