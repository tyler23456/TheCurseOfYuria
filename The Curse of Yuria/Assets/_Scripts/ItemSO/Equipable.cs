using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using System.Collections.ObjectModel;

public class Equipable : ItemBase, IItem
{
    [SerializeField] protected List<Modifier> modifiers;
    [SerializeField] protected List<Reactor> counters;
    [SerializeField] protected List<Reactor> interrupts;

    public ReadOnlyCollection<Modifier> getModifiers => modifiers.AsReadOnly();
    public ReadOnlyCollection<Reactor> getCounters => counters.AsReadOnly();
    public ReadOnlyCollection<Reactor> getInterrupts => interrupts.AsReadOnly();

    public override void Equip(IActor target)
    {
        foreach (Modifier modifier in modifiers)
            target.getStats.OffsetAttribute(modifier.getAttribute, modifier.getOffset);

        foreach (Reactor counter in counters)
            target.getCounters.Add(counter);

        foreach (Reactor interrupt in interrupts)
            target.getInterrupts.Add(interrupt);

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
        foreach (Modifier modifier in modifiers)
            target.getStats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);

        foreach (Reactor counter in counters)
            target.getCounters.Remove(counter);

        foreach (Reactor interrupt in interrupts)
            target.getInterrupts.Remove(interrupt);

        List<string> removedItems = new List<string>();

        removedItems = target.getEquipment.RemoveWhere(i =>
            ItemDatabase.Instance.GetTypeName(i) == itemType.name);

        target.getEquipment.Remove(name);
        target.obj.GetComponent<Character>().UnEquip(itemType.part);
    }
}
