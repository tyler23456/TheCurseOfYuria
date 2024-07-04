using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectProbability
{
    [SerializeField] IStatusEffect statusEffect;
    [Range(0, 1)][SerializeField] float probability = 1f;

    public IStatusEffect getStatusEffect => statusEffect;
    public float getProbability => probability;
}
