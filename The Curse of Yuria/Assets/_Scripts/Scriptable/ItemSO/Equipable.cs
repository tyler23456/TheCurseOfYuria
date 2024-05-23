using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;


public class Equipable : ItemBase, IItem
{
    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

        user.getAnimator.Attack();

        foreach (IActor target in targets)
            target.StartCoroutine(performAnimation(user, target));

        yield return null;
    }

    protected virtual IEnumerator performAnimation(IActor user, IActor target)
    {
        yield return new WaitForSeconds(0.5f);
        yield return PerformEffect(user, target);  
    }

    protected virtual IEnumerator PerformEffect(IActor user, IActor target)
    {
        if (user == null)
            yield break;

        float accumulator = 0;
        accumulator = _elementType.Calculate(user, target, power * IStats.powerMultiplier);
        accumulator = _armType.Calculate(user, target, accumulator);
        accumulator = _calculationType.Calculate(user, target, accumulator);

        CheckStatusEffects(target);
    }

    public override void Equip(IActor target)
    {
        base.Equip(target);

        IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        List<string> removedItems = new List<string>();

        switch (itemType.part)
        {
            case EquipmentPart.MeleeWeapon1H:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon1H ||
                factory.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon2H ||
                factory.GetItem(i).itemType.part == EquipmentPart.Bow);
                target.getAnimator.SetWeaponType(0);
                break;

            case EquipmentPart.MeleeWeapon2H:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon1H ||
                factory.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon2H ||
                factory.GetItem(i).itemType.part == EquipmentPart.Shield ||
                factory.GetItem(i).itemType.part == EquipmentPart.Bow);
                target.getAnimator.SetWeaponType(1);
                break;

            case EquipmentPart.Bow:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon1H ||
                factory.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon2H ||
                factory.GetItem(i).itemType.part == EquipmentPart.Shield ||
                factory.GetItem(i).itemType.part == EquipmentPart.Bow);
                target.getAnimator.SetWeaponType(3);
                break;

            case EquipmentPart.Shield:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon2H ||
                factory.GetItem(i).itemType.part == EquipmentPart.Shield ||
                factory.GetItem(i).itemType.part == EquipmentPart.Bow);
                break;

            default:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).itemType == itemType);
                break;
        }

        foreach (string removedItem in removedItems)
            factory.GetItem(removedItem).Unequip(target);

        target.getEquipment.Add(name);
        target.getCharacter.Equip(itemSprite, itemType.part);
    }

    public override void Unequip(IActor target)
    {
        base.Unequip(target);

        IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        List<string> removedItems = new List<string>();

        removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).itemType == itemType);

        target.getEquipment.Remove(name);
        target.getCharacter.UnEquip(itemType.part);
    }
}
