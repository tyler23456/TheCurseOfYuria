using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Interactables
{
    public class ItemBehaviour : InteractableWithIDBase, IInteractablePointer
    {
        protected void Start()
        {
            if (uniqueIdentifier.IsNotFoundInInventory())
                return;

            gameObject.SetActive(false);
        }

        public override void Interact(IActor player)
        {
            InventoryManager.Instance.AddItem(name, 60);
            uniqueIdentifier.AddToInventory();
            gameObject.SetActive(false);
        }
    }
}