using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCancellation", menuName = "StatusEffects/Cancellation")]
public class Cancellation : StatusEffectBase
{
    [SerializeField] List<ElementType> elementTypes;

    public override bool OnHit(IActor user, IActor target, IItem item)
    {
        if (item is not Skill)
            return false;

        Skill skill = (Skill)item;

        if (elementTypes.TrueForAll(i => i.name != skill.elementType.name))
        {
            user.getStatusEffects.Remove(name);
            return true;
        }
            
        return false;
    }
}
