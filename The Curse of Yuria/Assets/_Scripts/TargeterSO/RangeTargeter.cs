using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewRangeTargeter", menuName = "Targeters/RangeTargeter")]
public class RangeTargeter : TargeterBase
{
    [SerializeField] float targetDistanceOverride = DefaultTargetCheckDistance;

    public override IActor[] CalculateTargets(Vector2 position, float targetDistance = DefaultTargetCheckDistance)
    {
        base.CalculateTargets(position, targetDistanceOverride);
        return targets.ToArray();
    }
}
