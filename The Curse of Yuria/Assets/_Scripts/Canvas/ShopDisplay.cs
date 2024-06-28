using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopDisplay : DisplayBase
{
    public static ShopDisplay Instance { get; protected set; }

    [SerializeField] ItemDisplayAsset display; 

    [Header("Shop")]
    [SerializeField] Button buy;
    [SerializeField] Button sell;
    [SerializeField] Text selectedInfo;

    ItemTypeBase type;
    Dictionary<ItemTypeBase, Inventory> shopInventories = new Dictionary<ItemTypeBase, Inventory>();

    public float buyersRating { get; set; } = 1f;
    public float sellersRating { get; set; } = 1f;
    public Action<string> onClickGlobalItem { get; set; } = (itemName) => { };
    public List<(string name, int count)> itemsForSell { get; private set; } = new List<(string name, int count)>();

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        display.gameObject.SetActive(true);
        display.Initialize();

        display.helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.armorTab.GetComponent<PointerHover>().onPointerRightClick =  () => { };
        display.shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };

        shopInventories = new Dictionary<ItemTypeBase, Inventory>();
        shopInventories.Add(InventoryManager.Instance.helmetType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.melee1HType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.melee2HType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.armorType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.shieldType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.bowType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.scrollType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.basicType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.questItemType, new Inventory());

        foreach ((string name, int count) item in itemsForSell)
            shopInventories[ItemDatabase.Instance.Get(item.name).itemType].Add(item.name, item.count);

        buy.onClick.RemoveAllListeners();
        sell.onClick.RemoveAllListeners();

        buy.onClick.AddListener(OnBuy);
        sell.onClick.AddListener(OnSell);

        display.RefreshAllie(0);
        RefreshShopInventory(InventoryManager.Instance.helmetType);
        OnBuy();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        shopInventories.Clear();
        display.RefreshCurrency();
        itemsForSell.Clear();
        display.gameObject.SetActive(true);
    }

    private void Update()
    {
        display.UpdateAllieView();
    }

    void RefreshShopInventory(ItemTypeBase type)
    {
        this.type = type;
        display.RefreshGlobalInventory(shopInventories[type]);
    }

    void RefreshPlayerInventory(ItemTypeBase type)
    {
        this.type = type;
        display.RefreshGlobalInventory(InventoryManager.Instance.Get(type));
    }

    void OnBuy()
    {
        selectedInfo.text = "Buy an item";
        display.ClearTabListenters();

        display.helmetsTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.helmetType));
        display.meleeWeapons1HTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.melee1HType));
        display.meleeWeapons2HTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.melee2HType));
        display.armorTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.armorType));
        display.shieldsTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.shieldType));
        display.bowsTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.bowType));
        display.scrollsTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.scrollType));
        display.basicTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.bowType));
        display.questItemsTab.onClick.AddListener(() => RefreshShopInventory(InventoryManager.Instance.questItemType));

        display.RefreshGlobalInventory(shopInventories[type]);
    }

    void OnSell()
    {
        selectedInfo.text = "Sell an item";
        display.ClearTabListenters();

        display.helmetsTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.helmetType));
        display.meleeWeapons1HTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.melee1HType));
        display.meleeWeapons2HTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.melee2HType));
        display.armorTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.armorType));
        display.shieldsTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.shieldType));
        display.bowsTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.bowType));
        display.scrollsTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.scrollType));
        display.basicTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.bowType));
        display.questItemsTab.onClick.AddListener(() => RefreshPlayerInventory(InventoryManager.Instance.questItemType));

        display.onGlobalClick = OnSellItem;
        display.onEnterItem = display.ShowItemInfo;
        display.onEnterItem += display.RefreshStatusAttributes;

        display.RefreshGlobalInventory(InventoryManager.Instance.Get(type));
    }

    void OnBuyItem(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        InventoryManager.Instance.olms -= (int)(current.getValue * buyersRating);
        InventoryManager.Instance.AddItem(itemName);
        display.RefreshCurrency();
        display.RefreshGlobalInventory(shopInventories[current.itemType]);
    }

    void OnSellItem(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        InventoryManager.Instance.olms += (int)(current.getValue * sellersRating);
        InventoryManager.Instance.Get(current.itemType).Remove(itemName);
        display.RefreshCurrency();
        display.RefreshGlobalInventory(InventoryManager.Instance.Get(current.itemType));
    }

    void ShowPlayerProfit(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        int price = (int)(current.getValue * sellersRating);
        display.RefreshCurrency(price);
    }

    void ShowPlayerDeficit(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        int price = (int)(current.getValue * buyersRating);
        display.RefreshCurrency(price);
    }
}
