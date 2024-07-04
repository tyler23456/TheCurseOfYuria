using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCancellation", menuName = "StatusEffects/Cancellation")]
public class Cancellation : StatusEffectBase
{
    [SerializeField] List<ElementTypeSO> elementTypes;
    [SerializeField] List<ItemTypeBase> itemTypes;

    public override bool OnHit(IActor user, IActor target, IItem item)
    {
        if (item is not ISkill)
            return false;

        ISkill skill = (ISkill)item;

        if (itemTypes.TrueForAll(i => i.name != skill.type) || elementTypes.TrueForAll(i => i.name != skill.elementType.name))
        {
            user.getStatusEffects.Remove(name);
            return true;
        }
            
        return false;
    }
}
