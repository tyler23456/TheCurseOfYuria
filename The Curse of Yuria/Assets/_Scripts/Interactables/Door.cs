using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TCOY.Interactables
{
    public class Door : InteractableWithIDBase, IInteractableTrigger
    {
        [SerializeField] int sceneID;
        [SerializeField] Vector3 destination;
        [SerializeField] float eulerAngleY;
        [SerializeField] Sprite OpenDoor;
        [SerializeField] List<ItemSO> RequiredItems;
        [SerializeField] Prompt onLockedPrompt;

        public override string getAction => "";

        Sprite closedDoor;
        SpriteRenderer spriteRenderer;

        protected void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
                return;

            closedDoor = spriteRenderer.sprite;

            if (uniqueIdentifier.IsNotFoundInInventory())
                return;

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
            uniqueIdentifier.AddToInventory();

            Transform loadingDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/LoadingDisplay").transform;

            ILoadingData.sceneID = sceneID;
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