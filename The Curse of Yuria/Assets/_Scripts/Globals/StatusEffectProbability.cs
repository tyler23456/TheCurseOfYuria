using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffectProbability
{
    [SerializeField] public StatusEffect statusEffect;
    [SerializeField] [Range(0f, 1f)] public float probability = 1;
}
