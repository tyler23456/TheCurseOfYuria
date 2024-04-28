using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Chest : InteractableBase, IInteractable
    {
        [SerializeField] List<Item> items;
        [SerializeField] Sprite OpenChest;
        SpriteRenderer spriteRenderer;

        [SerializeField] List<string> RequiredItems;
        string onLockedInteractionPrompt = "This chest is locked.  It needs a key to open";

        protected new void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();

            if (global.getCompletedIds.Contains(getID))
                ShowOpenChestSprite();
        }

        public override void Interact(IPlayer player)
        {
            if (!RequiredItems.TrueForAll(i => global.getQuestItems.Contains(i)))
            {
                ShowLockedPrompt();
                return;
            }
            
            if (spriteRenderer.sprite == OpenChest)
                return;

            foreach (Item item in items)
                item.Interact(player);

            ShowOpenChestSprite();
            global.getCompletedIds.Add(getID, 1);
        }

        public void ShowOpenChestSprite()
        {
            spriteRenderer.sprite = OpenChest;
        }

        public void ShowLockedPrompt()
        {

        }
    }
}