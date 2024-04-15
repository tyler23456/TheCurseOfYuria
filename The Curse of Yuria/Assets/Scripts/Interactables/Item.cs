using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class Item : InteractableBase, IInteractable, IItem
    {
        [SerializeField] List<AttributeModifier> attributeModifiers;

        IGlobal global;
        bool isBeingUsed = false;
        bool isSupply = false;
        
        public void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
        }

        public override void Interact(Collider other)
        {
            base.Interact(other);
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