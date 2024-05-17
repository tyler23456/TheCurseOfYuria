using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IStatusEffects
{
    int getCount { get; }
    bool Contains(string name);
    void Add(string name, float accumulator = 0f);
    void AddRange(List<string> names);
    void Remove(string name);
    void RemoveRange(List<string> names);
    void RemoveAll();
    void RemoveWhere(Func<string, bool> predicate);
    string[] GetNames();
    float[] GetAccumulators();
    void SetNamesAndAccumulators(string[] names, float[] accumulators);
}
