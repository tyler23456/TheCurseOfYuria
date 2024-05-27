using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Door : InteractableBase, IContainer
    {
        [SerializeField] int sceneID;
        [SerializeField] Vector3 destination;
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

            if (spriteRenderer == null)
                return;

            closedDoor = spriteRenderer.sprite;

            if (Global.instance.getCompletedIds.Contains(getID))
                ShowOpenDoorSprite();
        }

        public override void Interact(IActor player)
        {
            if (!RequiredItems.TrueForAll(i => Global.instance.inventories[Factory.instance.getQuestItem.name].Contains(i.name)))
            {
                ShowLockedPrompt();
                return;
            }

            ShowOpenDoorSprite();
            Global.instance.getCompletedIds.Add(getID, 1);

            Global.instance.sceneIDToLoad = sceneID;
            Global.instance.scenePositionToStart = destination;
            Global.instance.sceneEulerAngleZToStart = eulerAngleZ;
            Global.instance.ToggleDisplay(Global.Display.Loading);
        }

        public void ShowOpenDoorSprite()
        {
            if (OpenDoor == null)
                return;

            spriteRenderer.sprite = OpenDoor;
        }

        public void ShowLockedPrompt()
        {
            Global.instance.cutsceneActions.Enqueue(onLockedPrompt);
            Global.instance.ToggleDisplay(Global.Display.Cutscene);
        }
    }
}