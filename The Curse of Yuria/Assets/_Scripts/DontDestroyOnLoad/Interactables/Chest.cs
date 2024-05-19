using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Chest : InteractableBase
    {
        [SerializeField] int sceneID;
        [SerializeField] Sprite open;
        [SerializeField] List<ItemBase> RequiredItems;
        [SerializeField] List<ItemBase> items; 
        [SerializeField] Prompt onLockedPrompt;

        Sprite closed;
        SpriteRenderer spriteRenderer;

        protected new void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();

            if (global.getCompletedIds.Contains(getID))
                ShowOpenChestSprite();
        }

        public override void Interact(IAllie player)
        {
            if (!RequiredItems.TrueForAll(i => global.inventories[IItem.Category.questItems].Contains(i.name)))
            {
                ShowLockedPrompt();
                return;
            }
            
            if (spriteRenderer.sprite == open)
                return;

            foreach (ItemBase item in items)
                global.inventories[item.category].Add(item.name);

            ShowOpenChestSprite();
            global.getCompletedIds.Add(getID, 1);
        }

        public void ShowOpenChestSprite()
        {
            spriteRenderer.sprite = open;
        }

        public void ShowLockedPrompt()
        {
            global.cutsceneActions.Enqueue(onLockedPrompt);
            global.ToggleDisplay(IGlobal.Display.CutsceneDisplay);
        }
    }
}