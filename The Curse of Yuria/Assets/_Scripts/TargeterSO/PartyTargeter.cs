using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPartyTargeter", menuName = "Targeters/PartyTargeter")]
public class PartyTargeter : TargeterBase
{
    public override IActor[] CalculateTargets(Vector2 position, float targetCheckDistance = DefaultTargetCheckDistance)
    {
        base.CalculateTargets(position);

        return targets.ToArray();
    }
}
