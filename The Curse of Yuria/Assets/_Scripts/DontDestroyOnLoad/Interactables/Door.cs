using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Door : InteractableBase
    {
        [SerializeField] int sceneID;
        [SerializeField] Vector2 destination;
        [SerializeField] float eulerAngleZ;
        [SerializeField] Sprite OpenDoor;
        [SerializeField] List<ItemBase> RequiredItems;
        [SerializeField] Prompt onLockedPrompt;

        Sprite closedDoor;
        SpriteRenderer spriteRenderer;

        protected new void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();
            closedDoor = spriteRenderer.sprite;

            if (global.getCompletedIds.Contains(getID))
                ShowOpenDoorSprite();
        }

        public override void Interact(IAllie player)
        {
            if (!RequiredItems.TrueForAll(i => global.inventories[IItem.Category.questItems].Contains(i.name)))
            {
                ShowLockedPrompt();
                return;
            }

            ShowOpenDoorSprite();
            global.getCompletedIds.Add(getID, 1);

            global.sceneIDToLoad = sceneID;
            global.scenePositionToStart = destination;
            global.sceneEulerAngleZToStart = eulerAngleZ;
            global.ToggleDisplay(IGlobal.Display.LoadingDisplay);
        }

        public void ShowOpenDoorSprite()
        {
            spriteRenderer.sprite = OpenDoor;
        }

        public void ShowLockedPrompt()
        {
            global.cutsceneActions.Enqueue(onLockedPrompt);
            global.ToggleDisplay(IGlobal.Display.CutsceneDisplay);
        }
    }
}