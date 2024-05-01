using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

namespace TCOY.DontDestroyOnLoad
{
    public class Equipment : ItemBase, IItem
    {
        public ItemSprite itemSprite { get { return base.itemSprite; } set { base.itemSprite = value; } }

        public override void Use(IActor user, IActor[] targets)
        {
            if (targets == null || targets.Length == 0 || targets[0] == null)
                return;

            IActor target = targets[0];

            if (target.getEquipment.Contains(name))
                Unequip(target);
            else
                Equip(target);
        }

        void Equip(IActor target)
        {
            foreach (Modifier modifier in modifiers)
                target.getStats.OffsetAttribute(modifier.getAttribute, modifier.getOffset);

            foreach (Reactor counter in counters)
                target.counters.Add(counter);

            foreach (Reactor interrupt in interrupts)
                target.interrupts.Add(interrupt);
        }

        void Unequip(IActor target)
        {
            foreach (Modifier modifier in modifiers)
                target.getStats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);

            foreach (Reactor counter in counters)
                target.counters.Remove(counter);

            foreach (Reactor interrupt in interrupts)
                target.interrupts.Remove(interrupt);
        }
    }
}