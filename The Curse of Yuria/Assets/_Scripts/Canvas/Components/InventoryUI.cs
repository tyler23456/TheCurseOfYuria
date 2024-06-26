using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryUI
{
    public Button buttonPrefab;
    public RectTransform grid { get; set; } = null;
    public Action<string> OnClick { get; set; } = (info) => { };
    public Action<string> onPointerEnter { get; set; } = (info) => { };
    public Action<string> onPointerExit { get; set; } = (info) => { };
    public IInventory inventory { get; set; } = null;

    public bool showName { get; set; } = false;
    public bool showCount { get; set; } = true;
    public bool showSprite { get; set; } = true;

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

            if (showSprite)
                button.transform.GetChild(1).GetComponent<Image>().sprite = ItemDatabase.Instance.Get(inventory.GetName(index)).icon;
            if (showCount)
                button.transform.GetChild(2).GetComponent<Text>().text = inventory.GetCount(index).ToString();
            if (showName)
                button.transform.GetChild(3).GetComponent<Text>().text = inventory.GetName(index);
        }

        showName = false;
        showCount = true;
    }

    public void EmptyDisplay()
    {
        if (grid == null)
            return;

        foreach (RectTransform child in grid)
            GameObject.Destroy(child.gameObject);
    }
}
