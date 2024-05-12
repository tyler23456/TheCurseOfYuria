using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffDebuff", menuName = "StatusEffects/BuffDebuff")]
public class Modification : StatusEffectBase, IStatusEffect
{
    [SerializeField] IStats.Attribute attribute;
    [SerializeField] int value;

    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);

        target.getStats.OffsetAttribute(attribute, value);
    }

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        target.getStats.OffsetAttribute(attribute, -value);
    }
}
