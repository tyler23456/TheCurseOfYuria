using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using System.Collections.ObjectModel;

namespace TCOY.Items
{
    public class EquipableBase : ItemBase, IEquipment, IItem
    {
        [SerializeField] EquipableInfo equipableInfo;

        public virtual EquipmentPart part => EquipmentPart.Armor;

        public ReadOnlyCollection<Modifier> getModifiers => equipableInfo.modifiers.AsReadOnly();
        public ReadOnlyCollection<Ward> getWards => equipableInfo.wards.AsReadOnly();
        public ReadOnlyCollection<Reactor> getCounters => equipableInfo.counters.AsReadOnly();
        public ReadOnlyCollection<Reactor> getInterrupts => equipableInfo.interrupts.AsReadOnly();

        public override void Equip(IActor user)
        {
            base.Equip(user);
            equipableInfo.Equip(user, name, part, itemSprite);
        }

        public override void Unequip(IActor user)
        {
            base.Unequip(user);
            equipableInfo.Unequip(user, name, part);
        }
    }
}
