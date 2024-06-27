using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedEntry : Entry
{
    [HideInInspector] [SerializeField] public string ID = "None";
}
