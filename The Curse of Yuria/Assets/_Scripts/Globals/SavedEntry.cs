using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedEntry : Entry
{
    [SerializeField] public string ID = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();
}
