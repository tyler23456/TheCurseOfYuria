using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPartyTargeter", menuName = "Targeters/PartyTargeter")]
public class PartyTargeter : TargeterBase
{
    public override List<IActor> CalculateTargets(Vector2 position)
    {
        base.CalculateTargets(position);

        return targets;
    }
}
