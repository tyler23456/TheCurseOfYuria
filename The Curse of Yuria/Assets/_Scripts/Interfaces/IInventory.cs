using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInventory
{
    int count { get; }
    void Add(string name, int count = 1);
    bool Remove(string name, int count = 1);
    string GetName(int index);
    int GetCount(int index);
    bool Contains(string name, int count = 1);
    string[] GetNames();
    int[] GetCounts();
    void SetNames(string[] names);
    void SetCounts(int[] counts);
    void Clear();
    string Find(Func<string, bool> predicate);
    List<string> RemoveWhere(Func<string, bool> predicate);
}
