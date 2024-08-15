using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomTargeter", menuName = "Targeters/RandomTargeter")]
public class RandomTargeter : TargeterBase
{
    public override IActor[] CalculateTargets(Vector2 position, float targetCheckDistance = DefaultTargetCheckDistance)
    {
        base.CalculateTargets(position);

        int index = Random.Range(0, targets.Count);

        return targets.Count == 0? new IActor[] { } : new IActor[] { targets[index] };
    }
}
