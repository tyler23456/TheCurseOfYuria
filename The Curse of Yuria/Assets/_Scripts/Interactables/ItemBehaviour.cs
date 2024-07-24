using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Interactables
{
    public class ItemBehaviour : InteractableWithIDBase, IInteractablePointer
    {
        public override string getAction => "Take ";

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
            InteractableSFXManager.Instance.PlayGrabItemSFX();
        }
    }
}