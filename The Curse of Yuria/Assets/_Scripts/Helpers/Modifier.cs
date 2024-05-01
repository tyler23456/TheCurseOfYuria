using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier : IModifier
{
    [SerializeField] IStats.Attributes attribute;
    [SerializeField] int offset;

    public IStats.Attributes getAttribute => attribute;
    public int getOffset => offset;
}
