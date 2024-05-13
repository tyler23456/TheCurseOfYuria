using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomTargeter", menuName = "Targeters/RandomTargeter")]
public class RandomTargeter : TargeterBase
{
    public override List<IActor> CalculateTargets(Vector2 position)
    {
        base.CalculateTargets(position);

        int index = Random.Range(0, targets.Count);

        return targets.Count == 0? null : new List<IActor> { targets[index] };
    }
}
