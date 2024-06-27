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

    Inventory inventory = new Inventory();
    InventoryUI inventoryUI = new InventoryUI();

    public Action<string> onClick { get; set; } = (itemName) => { };

    public Inventory getInventory => inventory;

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
        inventoryUI.inventory = inventory;
        inventoryUI.OnClick = OnClick;
        inventoryUI.OnClick += onClick;
        inventoryUI.onPointerEnter = (itemName) => { };
        inventoryUI.onPointerExit = (itemName) => { };
        inventoryUI.Display();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        inventory.Clear();
        onClick = (itemName) => { };
    }

    void OnClick(string itemName)
    {
        getInventory.Remove(itemName);
        InventoryManager.Instance.AddItem(itemName);

        OnRefresh();

        if (inventory.count == 0)
            gameObject.SetActive(false);
    }

    void OnExit()
    {
        gameObject.SetActive(false);
    }
}
