using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;
using System.Collections.ObjectModel;

public class EquipmentDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] Button buttonPrefab;

    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;
    [SerializeField] AudioClip equip;
    [SerializeField] AudioClip unequip;
    [SerializeField] AudioClip cyclePartyMembers;
    [SerializeField] AudioClip cycleEquipmentParts;

    [SerializeField] Camera detailedActorViewCamera;

    [SerializeField] RectTransform grid;

    [SerializeField] Button helmetsTab;
    [SerializeField] Button earringsTab;
    [SerializeField] Button glassesTab;
    [SerializeField] Button meleeWeapons1HTab;
    [SerializeField] Button meleeWeapons2HTab;
    [SerializeField] Button capesTab;
    [SerializeField] Button armorTab;
    [SerializeField] Button shieldsTab;
    [SerializeField] Button bowsTab;

    [SerializeField] Text itemName;
    [SerializeField] Image itemSprite;
    [SerializeField] Text itemInfo;

    [SerializeField] Text partyMemberName;
    [SerializeField] Text partyMemberStats;
    [SerializeField] Text partyMemberValues;
    [SerializeField] Text partyMemberIncreases;

    [SerializeField] Image helmetSlot;
    [SerializeField] Image earringSlot;
    [SerializeField] Image glassesSlot;
    [SerializeField] Image meleeWeapon1HSlot;
    [SerializeField] Image meleeWeapon2HSlot;
    [SerializeField] Image capeSlot;
    [SerializeField] Image armorSlot;
    [SerializeField] Image shieldSlot;
    [SerializeField] Image bowsSlot;

    [SerializeField] Sprite helmetSprite;
    [SerializeField] Sprite earringSprite;
    [SerializeField] Sprite glassesSprite;
    [SerializeField] Sprite meleeWeapon1HSprite;
    [SerializeField] Sprite meleeWeapon2HSprite;
    [SerializeField] Sprite capeSprite;
    [SerializeField] Sprite armorSprite;
    [SerializeField] Sprite shieldSprite;
    [SerializeField] Sprite bowsSprite;
    [SerializeField] Sprite EmptySprite;

    Dictionary<ItemTypeBase, Image> slots = new Dictionary<ItemTypeBase, Image>();

    ItemTypeBase currentType;
    int allieIndex = 0;

    IActor allie;

    IInventory equipment;
    IStats stats;

    IEquipment previousItem;
    IEquipment currentItem;

    ReadOnlyCollection<Modifier> oldModifiers;
    ReadOnlyCollection<Modifier> newModifiers;

    Modifier oldModifier;
    Modifier newModifier;
    int oldModifierValue = 0;
    int newModifierValue = 0;
    int modifierValue = 0;

    bool previousActive = true;

    InventoryUI globalInventoryUI;

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
        RefreshPartyMember();

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
        globalInventoryUI.grid = grid;
        globalInventoryUI.buttonPrefab = buttonPrefab;
        globalInventoryUI.inventory = InventoryManager.Instance.Get(type);
        globalInventoryUI.OnClick = (itemName) => OnEquip(itemName);
        globalInventoryUI.onPointerEnter = (itemName) => OnPointerEnter(itemName);
        globalInventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
        globalInventoryUI.Display();
    }

    public void RefreshPartyMember(int offset = 0)
    {
        allie?.obj.SetActive(previousActive);

        allieIndex--;
        allieIndex = Mathf.Clamp(allieIndex, 0, AllieManager.Instance.count - 1);

        allie = AllieManager.Instance[allieIndex];
        previousActive = allie.obj.activeSelf;
        allie.obj.SetActive(true);

        detailedActorViewCamera.cullingMask = (1 << allie.obj.transform.GetChild(0).gameObject.layer) 
            | ( 1 << LayerMask.NameToLayer("Light"));

        equipment = allie.getEquipment;
        stats = allie.getStats;

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

        for (int i = 0; i < equipment.count; i++)
        {
            itemType = ItemDatabase.Instance.GetType(equipment.GetName(i));
            slots[itemType].sprite = ItemDatabase.Instance.GetIcon(equipment.GetName(i));
        }
        partyMemberName.text = allie.obj.name;
        partyMemberStats.text = "";
        partyMemberValues.text = "";

        int[] statValues = stats.GetAttributes();
        for (int i = 0; i < statValues.Length; i++)
        {
            partyMemberStats.text += ((IStats.Attribute)i).ToString() + "\n";
            partyMemberValues.text += statValues[i].ToString() + "\n";
        }
    }

    public void OnUnequip(ItemTypeBase type)
    {
        string itemName = equipment.Find(i => ItemDatabase.Instance.GetTypeName(i) == type.name);

        if (itemName == null)
            return;

        currentItem = (IEquipment)ItemDatabase.Instance.Get(itemName);

        InventoryManager.Instance.Get(currentItem.itemType).Add(itemName);
        currentItem.Unequip(allie);
        AudioManager.Instance.PlaySFX(unequip);

        RefreshPartyMember();
        RefreshEquipment(currentItem.itemType);
    }

    public void OnEquip(string itemName)
    {
        currentItem = (IEquipment)ItemDatabase.Instance.Get(itemName);

        string partyMemberItem = equipment.Find(i => ItemDatabase.Instance.GetTypeName(i) == currentItem.itemType.name);

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

        RefreshPartyMember();
        RefreshEquipment(currentType);
    }

    public void OnPointerEnter(string itemName)
    {
        currentItem = (IEquipment)ItemDatabase.Instance.Get(itemName);

        string previousItemName = equipment.Find(i => ItemDatabase.Instance.GetTypeName(i) == currentItem.itemType.name);

        if (previousItemName == null)
            previousItem = (IEquipment)ItemDatabase.Instance.Get("Empty");
        else
            previousItem = (IEquipment)ItemDatabase.Instance.Get(previousItemName);


        this.itemName.text = itemName;
        this.itemInfo.text = currentItem.getInfo;
        partyMemberIncreases.text = "";
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
                partyMemberIncreases.text += "<color=#555555ff>" + modifierValue.ToString() + "\n" + "</color>";
            else if (modifierValue > 0)
                partyMemberIncreases.text += "<color=#00ff00ff>" + "+ " + modifierValue.ToString() + "\n" + "</color>";
            else
                partyMemberIncreases.text += "<color=#ff0000ff>" + "" + modifierValue.ToString() + "\n" + "</color>";
        }
    }

    public void OnPointerExit(string itemName)
    {
        this.itemName.text = "";
        itemInfo.text = "";
        partyMemberIncreases.text = "";
        this.itemSprite.sprite = EmptySprite;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {        
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshPartyMember(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshPartyMember(1);
        }

        //move Detailed Actor View Camera to the new character
        detailedActorViewCamera.transform.position = allie.obj.transform.position + new Vector3(0f, 1f, -2.5f);
        //---------------------------------------------------
    }
}
