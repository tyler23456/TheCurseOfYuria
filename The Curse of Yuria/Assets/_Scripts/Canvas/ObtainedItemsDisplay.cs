using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class ObtainedItemsDisplay : MenuBase
    {
        [SerializeField] RectTransform grid;
        [SerializeField] Button obtainedItemPrefab;
        [SerializeField] float displayTime = 7f;

        InventoryUI inventoryUI = new InventoryUI();

        float accumulator;

        protected new void OnEnable()
        {
            base.OnEnable();
            Global.instance.gameState = Global.GameState.Playing;

            Inventory inventory = new Inventory(Global.instance.obtainedItems.ToArray());
            Global.instance.obtainedItems.Clear();

            inventoryUI.grid = grid;
            inventoryUI.buttonPrefab = obtainedItemPrefab;
            inventoryUI.inventory = inventory;
            inventoryUI.OnClick = (itemName) => { };
            inventoryUI.onPointerEnter = (itemName) => { };
            inventoryUI.onPointerExit = (itemName) => { };
            inventoryUI.Display();

            accumulator = 0f;
        }

        protected new void OnDisable()
        {
            base.OnDisable();
        }

        void Update()
        {
            accumulator += Time.deltaTime;

            if (accumulator >= displayTime)
                gameObject.SetActive(false);
        }
    }
}