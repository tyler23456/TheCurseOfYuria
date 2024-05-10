using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyTargeter : TargeterBase
{
    public override List<IActor> GetTargets(Vector2 position)
    {
        base.GetTargets(position);



        return targets;
    }
}
