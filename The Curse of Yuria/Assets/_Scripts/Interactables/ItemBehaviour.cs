using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Interactables
{
    public class ItemBehaviour : InteractableWithIDBase, IInteractablePointer
    {
        public override void Interact(IActor player)
        {
            InventoryManager.Instance.AddItem(name, 60);
            InventoryManager.Instance.completedIds.Add(getID, 1);
            gameObject.SetActive(false);
        }
    }
}