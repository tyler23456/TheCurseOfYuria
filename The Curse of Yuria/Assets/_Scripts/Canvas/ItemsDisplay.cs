using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;
using System.Collections.ObjectModel;

public class ItemsDisplay : DisplayBase
{
    public static ItemsDisplay Instance { get; protected set; }

    [SerializeField] ItemDisplayAsset display;

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

        display.helmetsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.helmetType));
        display.meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HandedType));
        display.meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HandedType));
        display.armorTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.armorType));
        display.shieldsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.shieldType));
        display.bowsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.bowType));
        display.scrollsTab.onClick.AddListener(() => RefreshScrollsWithSFX(InventoryManager.Instance.scrollType));
        display.basicTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.basicType));
        display.questItemsTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.questItemType));

        display.helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.helmetType);
        display.meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.melee1HandedType);
        display.meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.melee2HandedType);
        display.armorTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.armorType);
        display.shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.shieldType);
        display.bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.bowType);
        display.scrollsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.basicTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.questItemsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };

        display.onEnterItem = display.ShowItemInfo;
        display.onEnterItem += display.RefreshAllieInfo;
        display.onExitItem = (n) => display.ClearItemInfo();
        display.onExitItem += (n) => display.RefreshAllieInfo("");

        display.RefreshAllie(0);

        RefreshEquipment(InventoryManager.Instance.helmetType);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        display.gameObject.SetActive(false);
    }

    private void Update()
    {
        display.UpdateAllieView();
    }

    void RefreshEquipmentWithSFX(string type)
    {
        MenuSFXManager.Instance.PlayChangeEquipmentPart();
        RefreshEquipment(type);
    }

    void RefreshEquipment(string type)
    {
        display.onGlobalClick = OnEquipEquipment;
        display.isRefreshingStatusAttributes = true;
        display.localInventoryGameObject.SetActive(false);
        display.SetGlobalInventoryBehavior();
        display.SetLocalInventoryBehavior();
        display.RefreshItemInfo(type);
    }

    void RefreshScrollsWithSFX(string type)
    {
        MenuSFXManager.Instance.PlayChangeEquipmentPart();
        display.onGlobalClick = OnEquipScroll;
        display.onLocalClick = OnUnequipScroll;
        display.isRefreshingStatusAttributes = false;
        display.localInventoryGameObject.SetActive(true);
        display.SetGlobalInventoryBehavior(showName: true);
        display.SetLocalInventoryBehavior(showName: true, showCount: false);
        display.RefreshItemInfo(type);
    }

    void RefreshReadonlyWithSFX(string type)
    {
        MenuSFXManager.Instance.PlayChangeEquipmentPart();
        display.onGlobalClick = (itemName) => { };
        display.isRefreshingStatusAttributes = false;
        display.localInventoryGameObject.SetActive(false);
        display.SetGlobalInventoryBehavior();
        display.SetLocalInventoryBehavior();
        display.RefreshItemInfo(type);
    }

    void OnEquipEquipment(string itemName)
    {
        IEquipment current = (IEquipment)ItemDatabase.Instance.Get(itemName);

        string previous = display.allie.getEquipment.Find(i => ItemDatabase.Instance.GetType(i) == current.type);

        if (previous == null)
        {
            InventoryManager.Instance.Get(current.type).Remove(itemName);
        }
        else
        {
            InventoryManager.Instance.Get(current.type).Add(previous);
            InventoryManager.Instance.Get(current.type).Remove(itemName);
        }

        current.Equip(display.allie);
        MenuSFXManager.Instance.PlayEquip();

        display.RefreshItemInfo(current.type);
    }

    void OnUnequipEquipment(string type)
    {
        string itemName = display.allie.getEquipment.Find(i => ItemDatabase.Instance.GetType(i) == type);

        if (itemName == null)
            return;

        IEquipment current = (IEquipment)ItemDatabase.Instance.Get(itemName);

        InventoryManager.Instance.Get(current.type).Add(itemName);
        current.Unequip(display.allie);
        MenuSFXManager.Instance.PlayUnequip();

        display.RefreshItemInfo(type);
    }

    protected void OnEquipScroll(string itemName)
    {
        if (display.allie.getScrolls.Contains(itemName))
            return;

        InventoryManager.Instance.scrolls.Remove(itemName);

        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Equip(display.allie);

        display.RefreshItemInfo(scroll.type);
    }

    protected void OnUnequipScroll(string itemName)
    {
        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Unequip(display.allie);

        InventoryManager.Instance.scrolls.Add(itemName);

        display.RefreshItemInfo(scroll.type);
    }
}
