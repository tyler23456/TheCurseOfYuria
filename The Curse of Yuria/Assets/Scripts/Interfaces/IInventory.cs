using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    string[] getAllItems();
    string[] getUnmarkedItems();
    void Add(string name, int count);
    void Remove(string name, int count = 1);
    void RemoveAt(int index, int count = 1);
}
