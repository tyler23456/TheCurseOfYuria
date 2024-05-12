using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffects
{
    int getCount { get; }
    bool Contains(string name);
    void Add(string name, float accumulator = 0f);
    void AddRange(List<string> names);
    void Remove(string name);
    void RemoveRange(List<string> names);
    string[] GetNames();
    void SetNamesAndAccumulators(string[] names, float[] accumulators);

}
