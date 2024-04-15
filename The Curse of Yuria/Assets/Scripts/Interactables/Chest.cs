using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class Chest : InteractableBase, IInteractable
    {
        [SerializeField] List<Item> items;
        [SerializeField] Sprite OpenChest;

        bool isAlreadyUsed = false;
       
        public override void Interact(IPlayer player)
        {
            if (isAlreadyUsed)
                return;

            base.Interact(player);

            foreach (Item item in items)
                item.Interact(player);

            GetComponent<SpriteRenderer>().sprite = OpenChest;
            isAlreadyUsed = true;
        }
    }
}