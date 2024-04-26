using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public int count { get; }
    public void Add(string name, int count = 1);
    public bool Remove(string name, int count = 1);
    public string GetName(int index);
    public int GetCount(int index);
    public bool Contains(string name, int count = 1);
    string[] GetNames();
    public int[] GetCounts();
    public void SetNames(string[] names);
    public void SetCounts(int[] counts);
}
