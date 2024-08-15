using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAllNearbyTargeter", menuName = "Targeters/AllNearbyTargeter")]
public class AllTargeter : TargeterBase
{
    [SerializeField] float targetDistanceOverride = DefaultTargetCheckDistance; 

    public override IActor[] CalculateTargets(Vector2 position, float targetDistance = DefaultTargetCheckDistance)
    {
        base.CalculateTargets(position, targetDistanceOverride);
        return targets.ToArray();
    }
}
