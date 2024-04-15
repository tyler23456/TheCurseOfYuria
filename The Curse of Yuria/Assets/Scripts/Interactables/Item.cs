using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class Item : InteractableBase, IInteractable, IItem
    {
        [SerializeField] List<AttributeModifier> attributeModifiers;

        bool isBeingUsed = false;
        bool isSupply = false;
        
        public new void Start()
        {
            base.Start();
        }

        public override void Interact(IPlayer player)
        {
            base.Interact(player);
        }

        public virtual void Use(IActor target)
        {
            isBeingUsed = !isBeingUsed;

            if (isBeingUsed)
            {
                foreach (AttributeModifier attributeModifier in attributeModifiers)
                    target.getStats.OffsetDynamicAttributeValue(attributeModifier.name, attributeModifier.value);
            }
            else
            {
                foreach (AttributeModifier attributeModifier in attributeModifiers)
                    target.getStats.OffsetDynamicAttributeValue(attributeModifier.name, -attributeModifier.value);
            }

            if (isSupply)
            {
                global.getSupplies.Remove(name);
            }
        }

        public class AttributeModifier
        {
            public string name = "";
            public int value = 1;
        }
    }
}