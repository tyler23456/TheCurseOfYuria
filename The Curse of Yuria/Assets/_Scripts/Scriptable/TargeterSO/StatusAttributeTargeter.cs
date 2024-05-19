using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatusAttributeTargeter", menuName = "Targeters/StatusAttributeTargeter")]
public class StatusAttributeTargeter : TargeterBase
{
    enum State { lowest, highest }

    [SerializeField] State state;
    [SerializeField] IStats.Attribute attribute;

    public override List<IActor> CalculateTargets(Vector2 position)
    {
        base.CalculateTargets(position);

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

        List<IActor> results = new List<IActor>();
        if (result != null)
            results.Add(result);

        return results;
    }
}
