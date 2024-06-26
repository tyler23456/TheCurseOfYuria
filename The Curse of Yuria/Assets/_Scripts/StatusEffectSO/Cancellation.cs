using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCancellation", menuName = "StatusEffects/Cancellation")]
public class Cancellation : StatusEffectBase
{
    [SerializeField] List<ElementTypeBase> elementTypes;
    [SerializeField] List<ItemTypeBase> itemTypes;

    public override bool ActivateCounter(IActor user, IActor target, IItem item)
    {
        if (item is not ISkill)
            return false;

        ISkill skill = (ISkill)item;

        if (itemTypes.TrueForAll(i => i.name != skill.itemType.name) || elementTypes.TrueForAll(i => i.name != skill.elementType.name))
        {
            user.getStatusEffects.Remove(name);
            return true;
        }
            
        return false;
    }
}
