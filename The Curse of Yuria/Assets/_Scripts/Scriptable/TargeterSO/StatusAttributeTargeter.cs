using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAttributeTargeter : TargeterBase
{
    enum State { lowest, highest }

    [SerializeField] State state;
    [SerializeField] IStats.Attribute attribute;

    public override List<IActor> GetTargets(Vector2 position)
    {
        base.GetTargets(position);

        int value = state == State.lowest? int.MaxValue : int.MinValue;
        IActor result = null;

        foreach (IActor target in targets)
        {
            if (state == State.lowest && target.getStats.GetAttribute(attribute) < value || state == State.highest && target.getStats.GetAttribute(attribute) > value)
            {
                value = target.getStats.GetAttribute(attribute);
                result = target;
            }
        }

        return result == null? null : new List<IActor> { result };
    }
}