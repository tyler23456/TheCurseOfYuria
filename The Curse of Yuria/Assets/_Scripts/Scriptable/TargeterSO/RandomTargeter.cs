using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTargeter : TargeterBase
{
    public override List<IActor> GetTargets(Vector2 position)
    {
        base.GetTargets(position);

        int index = Random.Range(0, targets.Count);

        return targets.Count == 0? null : new List<IActor> { targets[index] };
    }
}