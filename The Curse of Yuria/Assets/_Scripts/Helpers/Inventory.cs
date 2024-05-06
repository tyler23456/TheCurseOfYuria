using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class Inventory : IInventory
{
    List<string> names = new List<string>();
    List<int> counts = new List<int>();

    public int count => names.Count;

    public void Add(string name, int count = 1)
    {
        int index = names.IndexOf(name);

        if (index == -1)
        {
            names.Add(name);
            counts.Add(count);
        }
        else
        {
            counts[index] += count;
        }
    }

    public bool Remove(string name, int count = 1)
    {
        int index = names.IndexOf(name);

        if (index == -1 || counts[index] - count < 0)
        {
            return false;
        }
        else if (counts[index] - count == 0)
        {
            names.RemoveAt(index);
            counts.RemoveAt(index);
            return true;
        }
        else
        {
            counts[index] -= count;
            return true;
        }
    }

    public string GetName(int index)
    {
        return names[index];
    }

    public int GetCount(int index)
    {
        return counts[index];
    }

    public bool Contains(string name, int count = 1)
    {
        int index = names.IndexOf(name);

        if (index == -1 || counts[index] < count)
            return false;
        else
            return true;
    }

    public string Find(Func<string, bool> predicate)
    {
        foreach (string name in names)
            if (predicate(name))
                return name;
        return null;
    }

    public List<string> RemoveWhere(Func<string, bool> predicate)
    {
        List<int> indexes = new List<int>();
        List<string> results = new List<string>();

        for (int i = 0; i < names.Count; i++)
        {
            if (predicate(names[i]))
            {
                indexes.Add(i);
                results.Add(names[i]);
                names.RemoveAt(i);
                counts.RemoveAt(i);
                i--;
            }
        }
        return results;
    }

    public string[] GetNames()
    {
        return names.ToArray();
    }

    public int[] GetCounts()
    {
        return counts.ToArray();
    }

    public void SetNames(string[] names)
    {
        this.names.Clear();
        this.names.AddRange(names);
    }

    public void SetCounts(int[] counts)
    {
        this.counts.Clear();
        this.counts.AddRange(counts);
    }
}