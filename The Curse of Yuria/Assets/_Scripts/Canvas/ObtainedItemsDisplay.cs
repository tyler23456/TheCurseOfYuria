using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ObtainedItemsDisplay : DisplayBase
{
    public static ObtainedItemsDisplay Instance { get; protected set; }

    [SerializeField] RectTransform grid;
    [SerializeField] Button obtainedItemPrefab;
    [SerializeField] Button exitButton;

    InventoryUI inventoryUI = new InventoryUI();

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GameStateManager.Instance.Play();
        exitButton.onClick.AddListener(OnExit);
        OnRefresh();
    }

    public void OnRefresh()
    {
        inventoryUI.grid = grid;
        inventoryUI.buttonPrefab = obtainedItemPrefab;
        inventoryUI.inventory = IObtainedItemsData.inventory;
        inventoryUI.OnClick = OnClick;
        inventoryUI.OnClick += IObtainedItemsData.onClick;
        inventoryUI.onPointerEnter = (itemName) => { };
        inventoryUI.onPointerExit = (itemName) => { };
        inventoryUI.Display();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        IObtainedItemsData.inventory.Clear();
        IObtainedItemsData.onClick = (itemName) => { };
    }

    void OnClick(string itemName)
    {
        IObtainedItemsData.inventory.Remove(itemName);
        InventoryManager.Instance.AddItem(itemName);

        OnRefresh();

        if (IObtainedItemsData.inventory.count == 0)
            gameObject.SetActive(false);
    }

    void OnExit()
    {
        gameObject.SetActive(false);
    }
}
