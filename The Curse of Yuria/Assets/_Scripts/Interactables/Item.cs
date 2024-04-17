using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Interactables
{
    public class Item : InteractableBase, IInteractable, IItem
    {
        [SerializeField] List<AttributeModifier> attributeModifiers;

        bool isBeingUsed = false;
        bool isSupply = false;
        
        protected new void Start()
        {
            base.Start();

            if (global.getCompletedIds.Contains(getID))
                gameObject.SetActive(false);
        }

        public override void Interact(IPlayer player)
        {
            global.GetInventoryOf(name).Add(name, 1);
            gameObject.SetActive(false);
            global.getCompletedIds.Add(getID, 1);
        }

        public virtual void Use(IActor target)
        {
            isBeingUsed = !isBeingUsed;

            if (isBeingUsed)
            {
                foreach (AttributeModifier attributeModifier in attributeModifiers)
                    target.getStats.OffsetAddedAttributeValue(attributeModifier.name, attributeModifier.value);
            }
            else
            {
                foreach (AttributeModifier attributeModifier in attributeModifiers)
                    target.getStats.OffsetAddedAttributeValue(attributeModifier.name, -attributeModifier.value);
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