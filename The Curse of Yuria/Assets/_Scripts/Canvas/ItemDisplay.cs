using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemDisplay : ItemDisplayBase
{
    public static ItemDisplay Instance { get; protected set; }

    [Header("Shop")]
    [SerializeField] Button buy;
    [SerializeField] Button sell;
    [SerializeField] Text selectedInfo;
    [SerializeField] Text currencyLabel;
    [SerializeField] Text currencyValue;
    [SerializeField] Text currencyIncrease;

    bool isBuying = true;
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
        shopInventories = new Dictionary<ItemTypeBase, Inventory>();
        shopInventories.Add(InventoryManager.Instance.helmetType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.melee1HType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.melee2HType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.armorType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.shieldType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.bowType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.scrollType, new Inventory());
        shopInventories.Add(InventoryManager.Instance.basicType, new Inventory());

        foreach ((string name, int count) item in itemsForSell)
            shopInventories[ItemDatabase.Instance.Get(item.name).itemType].Add(item.name, item.count);

        base.OnEnable();

        buy.onClick.RemoveAllListeners();
        sell.onClick.RemoveAllListeners();

        buy.onClick.AddListener(OnClickBuy);
        sell.onClick.AddListener(OnClickSell);

        OnClickBuy();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        shopInventories.Clear();
        RefreshCurrency();
        itemsForSell.Clear();
    }


    protected override void RefreshEquipment(ItemTypeBase type)
    {
        currentType = type;
        if (isBuying)
        {
            RefreshGlobalEquipment(shopInventories[type]);
            globalInventoryUI.OnClick += onClickGlobalItem;
        }
        else
        {
            RefreshGlobalEquipment(InventoryManager.Instance.Get(type));
            globalInventoryUI.OnClick += onClickGlobalItem;
        }
    }

    protected void RefreshCurrency()
    {
        currencyIncrease.text = "";
        currencyValue.text = InventoryManager.Instance.olms.ToString();
    }

    protected void OnClickBuy()
    {
        isBuying = true;
        selectedInfo.text = "Buy an item";
        RefreshEquipmentWithSFX(currentType);
    }

    protected void OnClickSell()
    {
        isBuying = false;
        selectedInfo.text = "Sell an item";
        RefreshEquipmentWithSFX(currentType);
    }


    protected new void OnEquipEquipment(string itemName)
    {
        if (isBuying)
        {
            InventoryManager.Instance.olms -= (int)(currentItem.getValue * buyersRating);
            InventoryManager.Instance.AddItem(itemName);
        }
        else
        {
            InventoryManager.Instance.olms += (int)(currentItem.getValue * sellersRating);
            InventoryManager.Instance.Get(currentItem.itemType).Remove(itemName);
        }
        RefreshCurrency();
        RefreshEquipment(currentItem.itemType);
    }

    protected override void OnEnterGlobalEquipment(string itemName)
    {
        base.OnEnterGlobalEquipment(itemName);

        int price = 4;

        if (isBuying)
        {
            price = (int)(currentItem.getValue * buyersRating);
            currencyIncrease.text = "<color=#ff0000ff>" + "+ " + price.ToString() + "</color>";
        }  
        else
        {
            price = (int)(currentItem.getValue * sellersRating);
            currencyIncrease.text = "<color=#00ff00ff>" + "+ " + price.ToString() + "</color>";
        }
    }

    protected new void OnExitGlobalItem(string itemName)
    {
        base.OnExitGlobalItem(itemName);
        RefreshCurrency();
    }
}
