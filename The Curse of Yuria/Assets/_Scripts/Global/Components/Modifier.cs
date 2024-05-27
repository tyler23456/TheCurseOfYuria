using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    [SerializeField] IStats.Attribute attribute;
    [SerializeField] int offset;

    public IStats.Attribute getAttribute => attribute;
    public int getOffset => offset;
}
