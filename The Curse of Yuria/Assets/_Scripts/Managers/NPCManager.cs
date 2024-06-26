using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    void Awake()
    {
        instance = this;
    }

    public Transform Find(string name)
    {
        return transform.Find(name);
    }
}
