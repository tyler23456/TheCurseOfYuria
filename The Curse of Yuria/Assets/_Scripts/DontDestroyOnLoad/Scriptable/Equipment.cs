using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

namespace TCOY.DontDestroyOnLoad
{
    public class Equipment : ItemBase, IItem
    {
        [SerializeField] protected ItemSprite _itemSprites;
        [SerializeField] protected List<Modifier> modifiers;
        [SerializeField] protected List<Reactor> counters; //Reactor System
        [SerializeField] protected List<Reactor> interrupts;

        public ItemSprite itemSprite { get { return _itemSprites; } set { _itemSprites = value; } }

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

        [System.Serializable]
        public class Modifier : IModifier
        {
            [SerializeField] IStats.Attributes attribute;
            [SerializeField] int offset;

            public IStats.Attributes getAttribute => attribute;
            public int getOffset => offset;
        }

        public class Reactor : IReactor
        {
            [SerializeField] string trigger;
            [SerializeField] string reaction;

            public string getTrigger => trigger;
            public string getReaction => reaction;
        }
    }
}