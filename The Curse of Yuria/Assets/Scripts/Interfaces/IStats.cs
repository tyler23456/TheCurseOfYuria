using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    int GetStaticAttributeValue(string attributeName);
    int GetDynamicAttributeValue(string attributeName);
    void OffsetDynamicAttributeValue(string attributeName, int offsetValue);
    void ResetAll();
}
