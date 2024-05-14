using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewModifiction", menuName = "StatusEffects/Modication")]
public class Modification : StatusEffectBase, IStatusEffect
{
    [SerializeField] IStats.Attribute attribute;
    [SerializeField] int value;

    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);
        Destroy(Instantiate(visualEffect, target.getGameObject.transform), 3);
        target.getStats.OffsetAttribute(attribute, value);
    }

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        target.getStats.OffsetAttribute(attribute, -value);
    }
}
