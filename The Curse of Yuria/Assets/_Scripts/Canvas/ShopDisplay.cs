using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TCOY.Canvas
{
    public class ShopDisplay : DisplayBase
    {
        public static ShopDisplay Instance { get; protected set; }

        [SerializeField] ItemDisplayAsset display;

        [Header("Shop")]
        [SerializeField] Button buy;
        [SerializeField] Button sell;

        string type;
        Dictionary<string, Inventory> shopInventories = new Dictionary<string, Inventory>();

        public Action<string> onBuyItem { get; set; } = (itemName) => { };
        public Action<string> onSellItem { get; set; } = (itemName) => { };

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

            display.exitButton.onClick.RemoveAllListeners();
            display.exitButton.onClick.AddListener(OnExit);

            display.helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
            display.meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
            display.meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
            display.armorTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
            display.shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
            display.bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };

            shopInventories = new Dictionary<string, Inventory>();
            shopInventories.Add(InventoryManager.Instance.helmetType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.melee1HandedType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.melee2HandedType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.armorType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.shieldType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.bowType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.scrollType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.basicType, new Inventory());
            shopInventories.Add(InventoryManager.Instance.questItemType, new Inventory());

            for (int i = 0; i < IShopData.inventory.count; i++)
                shopInventories[ItemDatabase.Instance.Get(IShopData.inventory.GetName(i)).type].Add(IShopData.inventory.GetName(i), IShopData.inventory.GetCount(i));


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

            MenuSFXManager.Instance.PlayEquipmentMenuOpen();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            shopInventories.Clear();
            display.RefreshCurrency();
            IShopData.inventory.Clear();
            buy.transform.parent.gameObject.SetActive(false);
            sell.transform.parent.gameObject.SetActive(false);

            display.RefreshAllies();
            display.gameObject.SetActive(false);

            MenuSFXManager.Instance.PlayEquipmentMenuClose();
        }

        private void Update()
        {
            display.UpdateAllieView();
        }

        void OnExit()
        {
            gameObject.SetActive(false);
        }

        void RefreshEquipmentWithSFX(string type, Inventory inventory)
        {
            MenuSFXManager.Instance.PlayChangeEquipmentPart();
            RefreshEquipment(type, inventory);
        }

        void RefreshEquipment(string type, Inventory inventory)
        {
            this.type = type;
            display.isRefreshingStatusAttributes = true;
            display.localInventoryGameObject.SetActive(false);
            display.SetGlobalInventoryBehavior();
            display.SetLocalInventoryBehavior();
            display.RefreshItemInfo(type, inventory);
        }

        void RefreshScrollsWithSFX(string type, Inventory inventory)
        {
            MenuSFXManager.Instance.PlayChangeEquipmentPart();
            this.type = type;
            display.isRefreshingStatusAttributes = false;
            display.localInventoryGameObject.SetActive(true);
            display.SetGlobalInventoryBehavior(showName: true);
            display.SetLocalInventoryBehavior(showName: true, showCount: false);
            display.RefreshItemInfo(type, inventory);
        }

        void RefreshReadonlyWithSFX(string type, Inventory inventory)
        {
            MenuSFXManager.Instance.PlayChangeEquipmentPart();
            this.type = type;
            display.isRefreshingStatusAttributes = false;
            display.localInventoryGameObject.SetActive(false);
            display.SetGlobalInventoryBehavior();
            display.SetLocalInventoryBehavior();
            display.RefreshItemInfo(type, inventory);
        }

        void OnBuy()
        {
            display.selectionInfo.text = "Buy an item";
            display.ClearTabListenters();

            display.helmetsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.helmetType, shopInventories[InventoryManager.Instance.helmetType]));
            display.meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HandedType, shopInventories[InventoryManager.Instance.melee1HandedType]));
            display.meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HandedType, shopInventories[InventoryManager.Instance.melee2HandedType]));
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

            MenuSFXManager.Instance.PlayClick();
        }

        void OnSell()
        {
            display.selectionInfo.text = "Sell an item";
            display.ClearTabListenters();

            display.helmetsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.helmetType, InventoryManager.Instance.helmets));
            display.meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HandedType, InventoryManager.Instance.meleeWeapons1H));
            display.meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HandedType, InventoryManager.Instance.meleeWeapons2H));
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

            MenuSFXManager.Instance.PlayClick();
        }

        void OnBuyItem(string itemName)
        {
            IItem current = ItemDatabase.Instance.Get(itemName);

            int itemValue = (int)(current.marketValue * IShopData.buyersRating);

            if (itemValue > InventoryManager.Instance.olms)
            {
                NotificationManager.Instance.Notify("You do not have enough olms");
                return;
            }
                
            InventoryManager.Instance.olms -= itemValue;
            InventoryManager.Instance.AddItem(itemName);
            shopInventories[current.type].Remove(itemName);
            onBuyItem.Invoke(itemName);

            display.RefreshAllieInfo();
            display.RefreshGlobalInventory(shopInventories[current.type]);

            MenuSFXManager.Instance.PlayEquip();
        }

        void OnSellItem(string itemName)
        {
            IItem current = ItemDatabase.Instance.Get(itemName);
            InventoryManager.Instance.olms += (int)(current.marketValue * IShopData.sellersRating);
            shopInventories[current.type].Add(itemName);
            InventoryManager.Instance.Get(current.type).Remove(itemName);
            onSellItem.Invoke(itemName);

            display.RefreshAllieInfo();
            display.RefreshGlobalInventory(InventoryManager.Instance.Get(current.type));

            MenuSFXManager.Instance.PlayEquip();
        }

        void ShowPlayerProfit(string itemName)
        {
            IItem current = ItemDatabase.Instance.Get(itemName);
            int price = (int)(current.marketValue * IShopData.sellersRating);
            display.RefreshAllieInfo(itemName, price);
        }

        void ShowPlayerDeficit(string itemName)
        {
            IItem current = ItemDatabase.Instance.Get(itemName);
            int price = (int)(current.marketValue * IShopData.buyersRating);
            display.RefreshAllieInfo(itemName, -price);
        }
    }
}
