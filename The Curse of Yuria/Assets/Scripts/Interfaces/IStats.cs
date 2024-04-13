using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    int GetStaticAttributeValue(string attributeName);
    int GetDynamicAttributeValue(string attributeName);
    int OffsetDynamicAttributeValue(string attributeName, string offsetIdentifier, int offsetValue);
    int RemoveOffsetValue(string attributeName, string offsetIdentifier);
}
