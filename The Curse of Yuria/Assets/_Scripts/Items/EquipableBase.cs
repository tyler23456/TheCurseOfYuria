using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using System.Collections.ObjectModel;

namespace TCOY.Items
{
    public class EquipableBase : Equipable, IEquipment, IItem
    {
        [Space(5)] [SerializeField] protected EquipableInfo equipableInfo;

        public override string type => "";

        public override IEnumerator Use(IActor user, List<IActor> targets) { yield return null; }
        public override IEnumerator Use(IActor target) { yield return null; }

        public override EquipmentPart part => EquipmentPart.Armor;

        public override ReadOnlyCollection<Modifier> getModifiers => equipableInfo.modifiers.AsReadOnly();
        public override ReadOnlyCollection<Ward> getWards => equipableInfo.wards.AsReadOnly();
        public override ReadOnlyCollection<Reactor> getCounters => equipableInfo.counters.AsReadOnly();
        public override ReadOnlyCollection<Reactor> getInterrupts => equipableInfo.interrupts.AsReadOnly();

        public override List<string> Equip(IActor user)
        {
            return equipableInfo.Equip(user, name, part, itemSprite);
        }

        public override void Unequip(IActor user)
        {
            equipableInfo.Unequip(user, name, part);
        }

        public override List<string> GetRequiredRemovalsFor(IActor target)
        {
            return equipableInfo.GetRequiredRemovalsFor(target, name, part);
        }
    }
}
