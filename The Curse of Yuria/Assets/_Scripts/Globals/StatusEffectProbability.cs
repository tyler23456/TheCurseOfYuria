using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffectProbability
{
    [SerializeField] StatusEffectBase statusEffect;
    [Range(0, 1)][SerializeField] float probability = 1f;

    public StatusEffectBase getStatusEffect => statusEffect;
    public float getProbability => probability;
}
