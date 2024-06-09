using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Door : InteractableBase, IInteractableTrigger
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

            if (InventoryManager.Instance.completedIds.Contains(getID))
                ShowOpenDoorSprite();
        }

        public override void Interact(IActor player)
        {
            if (!RequiredItems.TrueForAll(i => InventoryManager.Instance.questItems.Contains(i.name)))
            {
                ShowLockedPrompt();
                return;
            }

            ShowOpenDoorSprite();
            InventoryManager.Instance.completedIds.Add(getID, 1);

            LoadingDisplay.Instance.ShowExclusivelyInParent(sceneID, destination, eulerAngleZ);
        }

        public void ShowOpenDoorSprite()
        {
            if (OpenDoor == null)
                return;

            spriteRenderer.sprite = OpenDoor;
        }

        public void ShowLockedPrompt()
        {
            CutsceneDisplay.Instance.ShowExclusivelyInParent(new ActionBase[] { onLockedPrompt });
        }


    }
}