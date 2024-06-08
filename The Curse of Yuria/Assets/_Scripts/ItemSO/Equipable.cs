using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;


public class Equipable : ItemBase, IItem
{

    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        SetDirection(user, targets);

        Animator animator = user.obj.GetComponent<Animator>();
        animator?.SetTrigger("Slash");

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

        foreach (BonusTypeBase bonusType in _bonusTypes)
            accumulator = bonusType.Calculate(user, target, accumulator);

        accumulator = _calculationType.Calculate(user, target, accumulator);

        CheckStatusEffects(target);
    }

    public override void Equip(IActor target)
    {
        base.Equip(target);

        List<string> removedItems = new List<string>();

        switch (itemType.part)
        {
            case EquipmentPart.MeleeWeapon1H:
                removedItems = target.getEquipment.RemoveWhere(i =>
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon1H ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon2H ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.Bow);
                target.obj.GetComponent<Animator>()?.SetInteger("WeaponType", 0);
                break;

            case EquipmentPart.MeleeWeapon2H:
                removedItems = target.getEquipment.RemoveWhere(i =>
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon1H ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon2H ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.Shield ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.Bow);
                target.obj.GetComponent<Animator>()?.SetInteger("WeaponType", 1);
                break;

            case EquipmentPart.Bow:
                removedItems = target.getEquipment.RemoveWhere(i =>
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon1H ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon2H ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.Shield ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.Bow);
                target.obj.GetComponent<Animator>()?.SetInteger("WeaponType", 3);
                break;

            case EquipmentPart.Shield:
                removedItems = target.getEquipment.RemoveWhere(i =>
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon2H ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.Shield ||
                ItemDatabase.Instance.GetPart(i) == EquipmentPart.Bow);
                break;

            default:
                removedItems = target.getEquipment.RemoveWhere(i =>
                ItemDatabase.Instance.GetTypeName(i) == itemType.name);
                break;
        }

        foreach (string removedItem in removedItems)
            ItemDatabase.Instance.Get(removedItem).Unequip(target);

        target.getEquipment.Add(name);
        target.obj.GetComponent<Character>()?.Equip(itemSprite, itemType.part);
    }

    public override void Unequip(IActor target)
    {
        base.Unequip(target);

        List<string> removedItems = new List<string>();

        removedItems = target.getEquipment.RemoveWhere(i =>
            ItemDatabase.Instance.GetTypeName(i) == itemType.name);

        target.getEquipment.Remove(name);
        target.obj.GetComponent<Character>().UnEquip(itemType.part);
    }
}
