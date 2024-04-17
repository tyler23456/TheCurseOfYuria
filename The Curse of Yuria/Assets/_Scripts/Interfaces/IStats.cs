using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    int GetBaseAttributeValue(string attributeName);
    int GetAddedAttributeValue(string attributeName);
    int GetTotalAttributeValue(string attributeName);
    void OffsetAddedAttributeValue(string attributeName, int offsetValue);
    void ResetAll();
    void ApplyPhysicalDamage(int attack, string type);
    void ApplyMagicalDamage(int attack, string type);
}
