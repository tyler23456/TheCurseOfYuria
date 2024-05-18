using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;


public class Equipable : ItemBase, IItem
{
    public override IEnumerator Use(IActor user, IActor[] targets)
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

        target.getStats.ApplyCalculation(power, user.getStats, group, type, element);
        CheckStatusEffects(target);
        global.successfulSubcommands.Add(new Subcommand(user, this, target));
    }

    public override void Equip(IActor target)
    {
        base.Equip(target);

        IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        List<string> removedItems = new List<string>();

        switch (category)
        {
            case IItem.Category.meleeWeapons1H:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).category == IItem.Category.meleeWeapons1H ||
                factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
                factory.GetItem(i).category == IItem.Category.bows);
                target.getAnimator.SetWeaponType(0);
                break;

            case IItem.Category.meleeWeapons2H:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).category == IItem.Category.meleeWeapons1H ||
                factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
                factory.GetItem(i).category == IItem.Category.shields ||
                factory.GetItem(i).category == IItem.Category.bows);
                target.getAnimator.SetWeaponType(1);
                break;

            case IItem.Category.bows:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).category == IItem.Category.meleeWeapons1H ||
                factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
                factory.GetItem(i).category == IItem.Category.shields ||
                factory.GetItem(i).category == IItem.Category.bows);
                target.getAnimator.SetWeaponType(3);
                break;

            case IItem.Category.shields:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).category == IItem.Category.meleeWeapons2H ||
                factory.GetItem(i).category == IItem.Category.shields ||
                factory.GetItem(i).category == IItem.Category.bows);
                break;

            default:
                removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).category == category);
                break;
        }

        foreach (string removedItem in removedItems)
            factory.GetItem(removedItem).Unequip(target);

        target.getEquipment.Add(itemName);
        target.getCharacter.Equip(itemSprite, IItem.partConverter[category]);
    }

    public override void Unequip(IActor target)
    {
        base.Unequip(target);

        IFactory factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        List<string> removedItems = new List<string>();

        removedItems = target.getEquipment.RemoveWhere(i =>
                factory.GetItem(i).category == category);

        target.getEquipment.Remove(itemName);
        target.getCharacter.UnEquip(IItem.partConverter[category]);
    }
}
