using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Ward
{
    [SerializeField] public ElementType elementType;
    [SerializeField] [Range(1, 500)] public int amount = 1;
}