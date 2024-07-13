using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class Door : InteractableBase, IInteractableTrigger
    {
        [SerializeField] string sceneName;
        [SerializeField] Vector3 destination;
        [SerializeField] float eulerAngleY;
        [SerializeField] Sprite OpenDoor;
        [SerializeField] List<ItemSO> RequiredItems;
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
                ActivateScriptedSequence(onLockedPrompt);
                return;
            }

            ShowOpenDoorSprite();
            InventoryManager.Instance.completedIds.Add(getID, 1);

            Transform loadingDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/LoadingDisplay").transform;

            loadingDisplay.GetChild(0).name = sceneName;
            loadingDisplay.gameObject.SetActive(true);

            SetPositionOfAllies(player, destination, new Vector3(0f, eulerAngleY, 0f));
        }

        public void ShowOpenDoorSprite()
        {
            if (OpenDoor == null)
                return;

            spriteRenderer.sprite = OpenDoor;
        }
    }
}