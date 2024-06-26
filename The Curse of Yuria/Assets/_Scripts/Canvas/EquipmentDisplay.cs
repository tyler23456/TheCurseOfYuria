using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;
using System.Collections.ObjectModel;

public class EquipmentDisplay : ItemDisplayBase
{
    public static EquipmentDisplay Instance { get; protected set; }

    [Header("Tabs")]
    [SerializeField] Button helmetsTab;
    [SerializeField] Button earringsTab;
    [SerializeField] Button glassesTab;
    [SerializeField] Button meleeWeapons1HTab;
    [SerializeField] Button meleeWeapons2HTab;
    [SerializeField] Button capesTab;
    [SerializeField] Button armorTab;
    [SerializeField] Button shieldsTab;
    [SerializeField] Button bowsTab;

    [Header("Slots")]
    [SerializeField] Image helmetSlot;
    [SerializeField] Image earringSlot;
    [SerializeField] Image glassesSlot;
    [SerializeField] Image meleeWeapon1HSlot;
    [SerializeField] Image meleeWeapon2HSlot;
    [SerializeField] Image capeSlot;
    [SerializeField] Image armorSlot;
    [SerializeField] Image shieldSlot;
    [SerializeField] Image bowsSlot;

    [Header("Sprites")]
    [SerializeField] Sprite helmetSprite;
    [SerializeField] Sprite earringSprite;
    [SerializeField] Sprite glassesSprite;
    [SerializeField] Sprite meleeWeapon1HSprite;
    [SerializeField] Sprite meleeWeapon2HSprite;
    [SerializeField] Sprite capeSprite;
    [SerializeField] Sprite armorSprite;
    [SerializeField] Sprite shieldSprite;
    [SerializeField] Sprite bowsSprite;

    Dictionary<ItemTypeBase, Image> slots = new Dictionary<ItemTypeBase, Image>();

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        globalInventoryUI = new InventoryUI();

        helmetsTab.onClick.RemoveAllListeners();
        earringsTab.onClick.RemoveAllListeners();
        glassesTab.onClick.RemoveAllListeners();
        meleeWeapons1HTab.onClick.RemoveAllListeners();
        meleeWeapons2HTab.onClick.RemoveAllListeners();
        capesTab.onClick.RemoveAllListeners();
        armorTab.onClick.RemoveAllListeners();
        shieldsTab.onClick.RemoveAllListeners();
        bowsTab.onClick.RemoveAllListeners();

        helmetsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.helmetType));
        earringsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.earringType));
        glassesTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.glassesType));
        meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HType));
        meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HType));
        capesTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.capeType));
        armorTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.armorType));
        shieldsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.shieldType));
        bowsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.bowType));

        helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.helmetType);
        earringsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.earringType);
        glassesTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.glassesType);
        meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.melee1HType);
        meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.melee2HType);
        capesTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.capeType);
        armorTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.armorType);
        shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.shieldType);
        bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequip(InventoryManager.Instance.bowType);

        slots.Clear();
        slots.Add(InventoryManager.Instance.helmetType, helmetSlot);
        slots.Add(InventoryManager.Instance.earringType, earringSlot);
        slots.Add(InventoryManager.Instance.glassesType, glassesSlot);
        slots.Add(InventoryManager.Instance.melee1HType, meleeWeapon1HSlot);
        slots.Add(InventoryManager.Instance.melee2HType, meleeWeapon2HSlot);
        slots.Add(InventoryManager.Instance.capeType, capeSlot);
        slots.Add(InventoryManager.Instance.armorType, armorSlot);
        slots.Add(InventoryManager.Instance.shieldType, shieldSlot);
        slots.Add(InventoryManager.Instance.bowType, bowsSlot);

        allieIndex = 0;
        RefreshEquipment(InventoryManager.Instance.helmetType);
        RefreshAllie();

        AudioManager.Instance.PlaySFX(open);
        GameStateManager.Instance.Pause();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void RefreshEquipmentWithSFX(ItemTypeBase part)
    {
        AudioManager.Instance.PlaySFX(cycleEquipmentParts);
        RefreshEquipment(part);
    }

    public void RefreshEquipment(ItemTypeBase type)
    {
        currentType = type;
        RefreshGlobalInventory(InventoryManager.Instance.Get(type));
    }

    public override void RefreshAllie(int offset = 0)
    {
        base.RefreshAllie(offset);

        globalInventory = allie.getEquipment;
        
        helmetSlot.sprite = helmetSprite;
        earringSlot.sprite = earringSprite;
        glassesSlot.sprite = glassesSprite;
        meleeWeapon1HSlot.sprite = meleeWeapon1HSprite;
        meleeWeapon2HSlot.sprite = meleeWeapon2HSprite;
        capeSlot.sprite = capeSprite;
        armorSlot.sprite = armorSprite;
        shieldSlot.sprite = shieldSprite;
        bowsSlot.sprite = bowsSprite;

        ItemTypeBase itemType = InventoryManager.Instance.helmetType;

        for (int i = 0; i < globalInventory.count; i++)
        {
            itemType = ItemDatabase.Instance.GetType(globalInventory.GetName(i));
            slots[itemType].sprite = ItemDatabase.Instance.GetIcon(globalInventory.GetName(i));
        }

        int[] statValues = stats.GetAttributes();
        for (int i = 0; i < statValues.Length; i++)
        {
            allieStats.text += ((IStats.Attribute)i).ToString() + "\n";
            allieValues.text += statValues[i].ToString() + "\n";
        }
    }

    public void OnUnequip(ItemTypeBase type)
    {
        string itemName = globalInventory.Find(i => ItemDatabase.Instance.GetTypeName(i) == type.name);

        if (itemName == null)
            return;

        currentItem = (IEquipment)ItemDatabase.Instance.Get(itemName);

        InventoryManager.Instance.Get(currentItem.itemType).Add(itemName);
        currentItem.Unequip(allie);
        AudioManager.Instance.PlaySFX(unequip);

        RefreshAllie();
        RefreshEquipment(currentItem.itemType);
        ClearItemAndAllieData();
    }

    public override void OnClickGlobalItem(string itemName)
    {
        currentItem = (IEquipment)ItemDatabase.Instance.Get(itemName);

        string partyMemberItem = globalInventory.Find(i => ItemDatabase.Instance.GetTypeName(i) == currentItem.itemType.name);

        if (partyMemberItem == null)
        {
            InventoryManager.Instance.Get(currentType).Remove(itemName);
        }
        else
        {
            InventoryManager.Instance.Get(currentType).Add(partyMemberItem);
            InventoryManager.Instance.Get(currentType).Remove(itemName);
        }

        currentItem.Equip(allie);
        AudioManager.Instance.PlaySFX(equip);

        RefreshAllie();
        RefreshEquipment(currentType);
        ClearItemAndAllieData();
    }

    public override void OnEnterGlobalItem(string itemName)
    {
        currentItem = (IEquipment)ItemDatabase.Instance.Get(itemName);

        string previousItemName = globalInventory.Find(i => ItemDatabase.Instance.GetTypeName(i) == currentItem.itemType.name);

        if (previousItemName == null)
            previousItem = (IEquipment)ItemDatabase.Instance.Get("Empty");
        else
            previousItem = (IEquipment)ItemDatabase.Instance.Get(previousItemName);


        this.itemName.text = itemName;
        this.itemInfo.text = currentItem.getInfo;
        allieIncreases.text = "";
        this.itemSprite.sprite = currentItem.icon;

        oldModifiers = previousItem.getModifiers;
        newModifiers = currentItem.getModifiers;

        if (currentItem.getCounters.Count > 0)
            this.itemInfo.text += "\n\nCounters:";

        foreach (Reactor reactor in currentItem.getCounters)
            this.itemInfo.text += "\n" + reactor.getItemName.ToString() + "|" + reactor.getMask.ToString() + "|" + reactor.getReaction.ToString() + "|" + reactor.getTargeter.ToString();

        if (currentItem.getCounters.Count > 0)
            this.itemInfo.text += "\n\nInterrupts:";

        foreach (Reactor reactor in currentItem.getInterrupts)
            this.itemInfo.text += "\n" + reactor.getItemName.ToString() + "|" + reactor.getMask.ToString() + "|" + reactor.getReaction.ToString() + "|" + reactor.getTargeter.ToString();

        int length = allie.getStats.GetAttributes().Length;

        for (int i = 0; i < length; i++)
        {
            oldModifier = oldModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);
            newModifier = newModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);

            if (oldModifier == null)
                oldModifierValue = 0;
            else
                oldModifierValue = oldModifier.getOffset;

            if (newModifier == null)
                newModifierValue = 0;
            else
                newModifierValue = newModifier.getOffset;

            modifierValue = newModifierValue - oldModifierValue;

            if (modifierValue == 0)
                allieIncreases.text += "<color=#555555ff>" + modifierValue.ToString() + "\n" + "</color>";
            else if (modifierValue > 0)
                allieIncreases.text += "<color=#00ff00ff>" + "+ " + modifierValue.ToString() + "\n" + "</color>";
            else
                allieIncreases.text += "<color=#ff0000ff>" + "" + modifierValue.ToString() + "\n" + "</color>";
        }
    }

    

    public override void OnExitGlobalItem(string itemName)
    {
        ClearItemAndAllieData();
    }
}
