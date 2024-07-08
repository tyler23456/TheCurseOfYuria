using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    [SerializeField] public IStats.Attribute attribute;
    [SerializeField] public int offset;
}
