using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TCOY.Canvas
{
    public class InventoryUI
    {
        public Button buttonPrefab;
        public RectTransform grid { get; set; } = null;
        public Action<string> OnClick { get; set; } = (info) => { };
        public Action<string> onPointerEnter { get; set; } = (info) => { };
        public Action<string> onPointerExit { get; set; } = (info) => { };
        public IInventory inventory { get; set; } = null;

        Button button = null;
        PointerHover pointerHover = null;

        public InventoryUI()
        {
        }

        public void Display()
        {
            EmptyDisplay();

            for (int i = 0; i < inventory.count; i++)
            {   
                int index = i;
                button = GameObject.Instantiate(buttonPrefab, grid);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => 
                { 
                    OnClick(inventory.GetName(index));
                });
                pointerHover = button.GetComponent<PointerHover>();
                pointerHover.onPointerEnter = () =>
                {
                    onPointerEnter.Invoke(inventory.GetName(index));
                };
                pointerHover.onPointerExit = () => onPointerExit.Invoke(inventory.GetName(index));
                button.transform.GetChild(1).GetComponent<Image>().sprite = Factory.instance.GetItem(inventory.GetName(index)).icon;
                button.transform.GetChild(2).GetComponent<Text>().text = inventory.GetCount(index).ToString();

            }
        }

        public void EmptyDisplay()
        {
            if (grid == null)
                return;

            foreach (RectTransform child in grid)
                GameObject.Destroy(child.gameObject);
        }


    }
}