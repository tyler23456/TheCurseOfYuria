using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;
using System.Collections.ObjectModel;

public class ItemDisplay : DisplayBase
{
    public static ItemDisplay Instance { get; protected set; }

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
        display.meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HType));
        display.meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HType));
        display.armorTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.armorType));
        display.shieldsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.shieldType));
        display.bowsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.bowType));
        display.scrollsTab.onClick.AddListener(() => RefreshScrollsWithSFX(InventoryManager.Instance.scrollType));
        display.basicTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.basicType));
        display.questItemsTab.onClick.AddListener(() => RefreshReadonlyWithSFX(InventoryManager.Instance.questItemType));

        display.helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.helmetType);
        display.meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.melee1HType);
        display.meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.melee2HType);
        display.armorTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.armorType);
        display.shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.shieldType);
        display.bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.bowType);
        display.scrollsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.basicTab.GetComponent<PointerHover>().onPointerRightClick = () => { };
        display.questItemsTab.GetComponent<PointerHover>().onPointerRightClick = () => { };

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

    void RefreshEquipmentWithSFX(ItemTypeBase type)
    {
        AudioManager.Instance.PlaySFX(display.cycleEquipmentParts);
        RefreshEquipment(type);
    }

    void RefreshEquipment(ItemTypeBase type)
    {
        display.onEnterItem = display.ShowItemInfo;
        display.onEnterItem += display.RefreshStatusAttributes;
        display.onExitItem = (n) => display.ClearItemInfo();
        display.onExitItem = (n) => display.RefreshStatusAttributes("");
        display.onGlobalClick = OnEquipEquipment;
        display.RefreshEquipment(type);
        display.localInventoryGameObject.SetActive(false);
    }

    void RefreshScrollsWithSFX(ItemTypeBase type)
    {
        AudioManager.Instance.PlaySFX(display.cycleEquipmentParts);
        display.onEnterItem = display.ShowItemInfo;
        display.onExitItem = display.ClearAllieAndItemInfo;
        display.onGlobalClick = OnEquipScroll;
        display.RefreshNonEquipment(type);
        display.localInventoryGameObject.SetActive(true);
    }

    void RefreshReadonlyWithSFX(ItemTypeBase type)
    {
        AudioManager.Instance.PlaySFX(display.cycleEquipmentParts);
        display.onEnterItem = display.ShowItemInfo;
        display.onExitItem = display.ClearAllieAndItemInfo;
        display.onGlobalClick = (itemName) => { };
        display.RefreshNonEquipment(type);
        display.localInventoryGameObject.SetActive(false);
    }

    void OnEquipEquipment(string itemName)
    {
        IEquipment current = (IEquipment)ItemDatabase.Instance.Get(itemName);

        string previous = display.allie.getEquipment.Find(i => ItemDatabase.Instance.GetTypeName(i) == current.itemType.name);

        if (previous == null)
        {
            InventoryManager.Instance.Get(current.itemType).Remove(itemName);
        }
        else
        {
            InventoryManager.Instance.Get(current.itemType).Add(previous);
            InventoryManager.Instance.Get(current.itemType).Remove(itemName);
        }

        current.Equip(display.allie);
        AudioManager.Instance.PlaySFX(display.equip);

        display.RefreshEquipment(current.itemType);
    }

    void OnUnequipEquipment(ItemTypeBase type)
    {
        string itemName = display.allie.getEquipment.Find(i => ItemDatabase.Instance.GetTypeName(i) == type.name);

        if (itemName == null)
            return;

        IEquipment current = (IEquipment)ItemDatabase.Instance.Get(itemName);

        InventoryManager.Instance.Get(current.itemType).Add(itemName);
        current.Unequip(display.allie);
        AudioManager.Instance.PlaySFX(display.unequip);

        display.RefreshEquipment(type);
    }

    protected void OnEquipScroll(string itemName)
    {
        InventoryManager.Instance.scrolls.Remove(itemName);

        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Equip(display.allie);

        display.RefreshNonEquipment(scroll.itemType);
    }

    protected void OnUnequipScroll(string itemName)
    {
        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Unequip(display.allie);

        InventoryManager.Instance.scrolls.Add(itemName);

        display.RefreshNonEquipment(scroll.itemType);
    }
}
