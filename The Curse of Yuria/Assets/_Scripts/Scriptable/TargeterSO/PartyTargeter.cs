using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPartyTargeter", menuName = "Targeters/PartyTargeter")]
public class PartyTargeter : TargeterBase
{
    public override List<IActor> GetTargets(Vector2 position)
    {
        base.GetTargets(position);



        return targets;
    }
}
