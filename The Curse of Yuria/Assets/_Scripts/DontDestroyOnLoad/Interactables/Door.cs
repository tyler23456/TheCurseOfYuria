using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Door : InteractableBase
    {
        [SerializeField] int sceneID;
        [SerializeField] Vector2 destination;
        [SerializeField] Sprite OpenDoor;
        SpriteRenderer spriteRenderer;

        [SerializeField] List<string> RequiredItems;
        string onLockedInteractionPrompt = "This door is locked.  It needs a key to open";

        protected new void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();

            if (global.getCompletedIds.Contains(getID))
                ShowOpenDoorSprite();
        }

        public override void Interact(IPartyMember player)
        {
            if (!RequiredItems.TrueForAll(i => global.inventories[IItem.Category.questItems].Contains(i)))
            {
                ShowLockedPrompt();
                return;
            }

            ShowOpenDoorSprite();
            global.getCompletedIds.Add(getID, 1);

            //door takes characters to another location or another scene
        }

        public void ShowOpenDoorSprite()
        {
            spriteRenderer.sprite = OpenDoor;
        }

        public void ShowLockedPrompt()
        {

        }

        public void MovePlayerToDestination()
        {

        }
    }
}