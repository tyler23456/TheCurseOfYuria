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
    public float sellersRating { get; set; } = 1f / 2f;
    public Action<string> onBuyItem { get; set; } = (itemName) => { };
    public Action<string> onSellItem { get; set; } = (itemName) => { };
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

        buy.transform.parent.gameObject.SetActive(true);
        sell.transform.parent.gameObject.SetActive(true);

        display.onLocalClick = (itemName) => { };

        display.RefreshAllie(0);
        RefreshEquipment(InventoryManager.Instance.helmetType, shopInventories[InventoryManager.Instance.helmetType]);
        OnBuy();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        shopInventories.Clear();
        display.RefreshCurrency();
        itemsForSell.Clear();
        buy.transform.parent.gameObject.SetActive(false);
        sell.transform.parent.gameObject.SetActive(false);
        display.gameObject.SetActive(true);
    }

    private void Update()
    {
        display.UpdateAllieView();
    }

    void RefreshEquipmentWithSFX(ItemTypeBase type, Inventory inventory)
    {
        AudioManager.Instance.PlaySFX(display.cycleEquipmentParts);
        RefreshEquipment(type, inventory);
    }

    void RefreshEquipment(ItemTypeBase type, Inventory inventory)
    {
        this.type = type;
        display.isRefreshingStatusAttributes = true;
        display.localInventoryGameObject.SetActive(false);
        display.SetGlobalInventoryBehavior();
        display.SetLocalInventoryBehavior();
        display.RefreshItemInfo(type, inventory);
    }

    void RefreshScrollsWithSFX(ItemTypeBase type, Inventory inventory)
    {
        AudioManager.Instance.PlaySFX(display.cycleEquipmentParts);
        this.type = type;      
        display.isRefreshingStatusAttributes = false;
        display.localInventoryGameObject.SetActive(true);
        display.SetGlobalInventoryBehavior(showName: true);
        display.SetLocalInventoryBehavior(showName: true, showCount: false);
        display.RefreshItemInfo(type, inventory);
    }

    void RefreshReadonlyWithSFX(ItemTypeBase type, Inventory inventory)
    {
        AudioManager.Instance.PlaySFX(display.cycleEquipmentParts);
        this.type = type;        
        display.isRefreshingStatusAttributes = false;
        display.localInventoryGameObject.SetActive(false);
        display.SetGlobalInventoryBehavior();
        display.SetLocalInventoryBehavior();
        display.RefreshItemInfo(type, inventory);
    }

    void OnBuy()
    {
        selectedInfo.text = "Buy an item";
        display.ClearTabListenters();

        display.helmetsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.helmetType, shopInventories[InventoryManager.Instance.helmetType]));
        display.meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HType, shopInventories[InventoryManager.Instance.melee1HType]));
        display.meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HType, shopInventories[InventoryManager.Instance.melee2HType]));
        display.armorTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.armorType, shopInventories[InventoryManager.Instance.armorType]));
        display.shieldsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.shieldType, shopInventories[InventoryManager.Instance.shieldType]));
        display.bowsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.bowType, shopInventories[InventoryManager.Instance.bowType]));
        display.scrollsTab.onClick.AddListener(() => RefreshScrollsWithSFX(InventoryManager.Instance.scrollType, shopInventories[InventoryManager.Instance.scrollType]));
        display.basicTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.basicType, shopInventories[InventoryManager.Instance.basicType]));
        display.questItemsTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.questItemType, shopInventories[InventoryManager.Instance.questItemType]));

        display.onGlobalClick = OnBuyItem;
        display.onEnterItem = display.ShowItemInfo;
        display.onEnterItem += display.RefreshAllieInfo;
        display.onEnterItem += ShowPlayerDeficit;
        display.onExitItem = (n) => display.ClearItemInfo();
        display.onExitItem += (n) => display.RefreshAllieInfo("");

        display.RefreshGlobalInventory(shopInventories[type]); //might need to change this
    }

    void OnSell()
    {
        selectedInfo.text = "Sell an item";
        display.ClearTabListenters();

        display.helmetsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.helmetType, InventoryManager.Instance.helmets));
        display.meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HType, InventoryManager.Instance.meleeWeapons1H));
        display.meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HType, InventoryManager.Instance.meleeWeapons2H));
        display.armorTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.armorType, InventoryManager.Instance.armor));
        display.shieldsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.shieldType, InventoryManager.Instance.shields));
        display.bowsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.bowType, InventoryManager.Instance.bows));
        display.scrollsTab.onClick.AddListener(() => RefreshScrollsWithSFX(InventoryManager.Instance.scrollType, InventoryManager.Instance.scrolls));
        display.basicTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.basicType, InventoryManager.Instance.basic));
        display.questItemsTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.questItemType, InventoryManager.Instance.questItems));

        display.onGlobalClick = OnSellItem;
        display.onEnterItem = display.ShowItemInfo;
        display.onEnterItem += display.RefreshAllieInfo;
        display.onEnterItem += ShowPlayerProfit;
        display.onExitItem = (n) => display.ClearItemInfo();
        display.onExitItem += (n) => display.RefreshAllieInfo("");

        display.RefreshGlobalInventory(InventoryManager.Instance.Get(type));
    }

    void OnBuyItem(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        InventoryManager.Instance.olms -= (int)(current.getValue * buyersRating);
        InventoryManager.Instance.AddItem(itemName);
        shopInventories[current.itemType].Remove(itemName);
        onBuyItem.Invoke(itemName);

        display.RefreshAllieInfo();
        display.RefreshGlobalInventory(shopInventories[current.itemType]);
    }

    void OnSellItem(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        InventoryManager.Instance.olms += (int)(current.getValue * sellersRating);
        shopInventories[current.itemType].Add(itemName);
        InventoryManager.Instance.Get(current.itemType).Remove(itemName);
        onSellItem.Invoke(itemName);

        display.RefreshAllieInfo();
        display.RefreshGlobalInventory(InventoryManager.Instance.Get(current.itemType));
    }

    void ShowPlayerProfit(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        int price = (int)(current.getValue * sellersRating);
        display.RefreshAllieInfo(itemName, price);
    }

    void ShowPlayerDeficit(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);
        int price = (int)(current.getValue * buyersRating);
        display.RefreshAllieInfo(itemName, -price);
    }
}
