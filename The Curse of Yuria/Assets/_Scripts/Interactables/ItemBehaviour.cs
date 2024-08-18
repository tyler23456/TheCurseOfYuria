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

            HideInteraction();
        }

        public override void Interact(IActor player)
        {
            InventoryManager.Instance.AddItem(name, 1);
            uniqueIdentifier.AddToInventory();
            InteractableSFXManager.Instance.PlayGrabItemSFX(GetComponent<AudioSource>());
            HideInteraction();
            
        }

        void HideInteraction()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }

    }
}