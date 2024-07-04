using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using System.Collections.ObjectModel;

namespace TCOY.Items
{
    [System.Serializable]
    public class EquipableInfo
    {
        [SerializeField] public List<Modifier> modifiers;
        [SerializeField] public List<Ward> wards;
        [SerializeField] public List<Reactor> counters;
        [SerializeField] public List<Reactor> interrupts;

        public ReadOnlyCollection<Modifier> getModifiers => modifiers.AsReadOnly();
        public ReadOnlyCollection<Ward> getWards => wards.AsReadOnly();
        public ReadOnlyCollection<Reactor> getCounters => counters.AsReadOnly();
        public ReadOnlyCollection<Reactor> getInterrupts => interrupts.AsReadOnly();

        public void Equip(IActor target, string equipmentName, EquipmentPart part, ItemSprite equipmentSprite)
        {
            foreach (Modifier modifier in modifiers)
                target.getStats.OffsetAttribute(modifier.getAttribute, modifier.getOffset);

            foreach (Ward ward in wards)
                target.getStats.OffsetWeakness(ward.getElementType.weaknessIndex, ward.getAmount);

            foreach (Reactor counter in counters)
                target.getCounters.Add(counter);

            foreach (Reactor interrupt in interrupts)
                target.getInterrupts.Add(interrupt);

            List<string> removedItems = new List<string>();

            switch (part)
            {
                case EquipmentPart.MeleeWeapon1H:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon1H ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon2H ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.Bow);
                    target.obj.GetComponent<Animator>()?.SetInteger("WeaponType", 0);
                    break;

                case EquipmentPart.MeleeWeapon2H:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon1H ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon2H ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.Shield ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.Bow);
                    target.obj.GetComponent<Animator>()?.SetInteger("WeaponType", 1);
                    break;

                case EquipmentPart.Bow:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon1H ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon2H ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.Shield ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.Bow);
                    target.obj.GetComponent<Animator>()?.SetInteger("WeaponType", 3);
                    break;

                case EquipmentPart.Shield:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    ItemDatabase.Instance.Part(i) == EquipmentPart.MeleeWeapon2H ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.Shield ||
                    ItemDatabase.Instance.Part(i) == EquipmentPart.Bow);
                    break;

                default:
                    removedItems = target.getEquipment.RemoveWhere(i =>
                    ItemDatabase.Instance.Part(i) == part);
                    break;
            }

            foreach (string removedItem in removedItems)
                ItemDatabase.Instance.Get(removedItem).Unequip(target);

            target.getEquipment.Add(equipmentName);
            target.obj.GetComponent<Character>()?.Equip(equipmentSprite, part);
        }

        public void Unequip(IActor target, string equipmentName, EquipmentPart part)
        {
            foreach (Modifier modifier in modifiers)
                target.getStats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);

            foreach (Ward ward in wards)
                target.getStats.OffsetWeakness(ward.getElementType.weaknessIndex, -ward.getAmount);

            foreach (Reactor counter in counters)
                target.getCounters.Remove(counter);

            foreach (Reactor interrupt in interrupts)
                target.getInterrupts.Remove(interrupt);

            List<string> removedItems = new List<string>();

            removedItems = target.getEquipment.RemoveWhere(i =>
                ItemDatabase.Instance.Part(i) == part);

            target.getEquipment.Remove(equipmentName);
            target.obj.GetComponent<Character>().UnEquip(part);
        }
    }
}
