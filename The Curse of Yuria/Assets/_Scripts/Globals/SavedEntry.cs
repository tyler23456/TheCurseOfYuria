using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedEntry
{
    public string ID = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();
    public IItem item;
    public int count = 1;
}
