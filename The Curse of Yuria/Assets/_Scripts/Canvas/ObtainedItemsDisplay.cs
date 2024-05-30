using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObtainedItemsDisplay : DisplayBase
{
    public static ObtainedItemsDisplay Instance { get; protected set; }

    [SerializeField] RectTransform grid;
    [SerializeField] Button obtainedItemPrefab;
    [SerializeField] float displayTime = 7f;

    Inventory inventory = new Inventory();
    InventoryUI inventoryUI = new InventoryUI();

    float accumulator;

    public Inventory getInventory => inventory;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Global.Instance.gameState = Global.GameState.Playing;

        inventoryUI.grid = grid;
        inventoryUI.buttonPrefab = obtainedItemPrefab;
        inventoryUI.inventory = inventory;
        inventoryUI.OnClick = (itemName) => { };
        inventoryUI.onPointerEnter = (itemName) => { };
        inventoryUI.onPointerExit = (itemName) => { };
        inventoryUI.Display();

        inventory.Clear();
        accumulator = 0f;
    }

    protected override void OnDisable()
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
