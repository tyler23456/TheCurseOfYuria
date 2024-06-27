using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.ObjectModel;

public class ItemDisplayBase : DisplayBase
{
    public enum State { equipping, shopping }

    public State state = State.equipping;

    [SerializeField] ScrollComponent scrollComponent;
    [SerializeField] EquipmentComponent equipmentComponent;
    [SerializeField] ReadonlyComponent readonlyComponent;
    [SerializeField] ReadonlyComponent shopComponent;

    [Header("Prefabs")]
    [SerializeField] protected Button buttonPrefab;
    [SerializeField] protected Sprite emptySprite;

    [Header("SFX")]
    [SerializeField] protected AudioClip open; // will eventually become scriptable objects
    [SerializeField] protected AudioClip close;
    [SerializeField] protected AudioClip equip;
    [SerializeField] protected AudioClip unequip;
    [SerializeField] protected AudioClip cyclePartyMembers;
    [SerializeField] protected AudioClip cycleEquipmentParts;

    [Header("Camera And Grids")]
    [SerializeField] protected Camera detailedActorViewCamera;
    [SerializeField] protected RectTransform globalInventoryGrid;
    [SerializeField] protected RectTransform allieInventoryGrid;

    [Header("Allie Info")]
    [SerializeField] protected Text allieName;
    [SerializeField] protected Text allieStats;
    [SerializeField] protected Text allieValues;
    [SerializeField] protected Text allieIncreases;

    [Header("Item Info")]
    [SerializeField] protected Text itemName;
    [SerializeField] protected Image itemSprite;
    [SerializeField] protected Text itemInfo;

    [Header("Tabs")]
    [SerializeField] protected Button helmetsTab;
    [SerializeField] protected Button meleeWeapons1HTab;
    [SerializeField] protected Button meleeWeapons2HTab;
    [SerializeField] protected Button armorTab;
    [SerializeField] protected Button shieldsTab;
    [SerializeField] protected Button bowsTab;
    [SerializeField] protected Button scrollsTab;
    [SerializeField] protected Button basicTab;
    [SerializeField] protected Button questItemsTab;

    [Header("Slots")]
    [SerializeField] Image helmetSlot;
    [SerializeField] Image meleeWeapon1HSlot;
    [SerializeField] Image meleeWeapon2HSlot;
    [SerializeField] Image armorSlot;
    [SerializeField] Image shieldSlot;
    [SerializeField] Image bowsSlot;

    [Header("Sprites")]
    [SerializeField] Sprite helmetSprite;
    [SerializeField] Sprite meleeWeapon1HSprite;
    [SerializeField] Sprite meleeWeapon2HSprite;
    [SerializeField] Sprite armorSprite;
    [SerializeField] Sprite shieldSprite;
    [SerializeField] Sprite bowsSprite;

    Dictionary<ItemTypeBase, Image> slots = new Dictionary<ItemTypeBase, Image>();

    protected ReadOnlyCollection<Modifier> oldModifiers;
    protected ReadOnlyCollection<Modifier> newModifiers;
    protected Modifier oldModifier;
    protected Modifier newModifier;
    protected int oldModifierValue = 0;
    protected int newModifierValue = 0;
    protected int modifierValue = 0;

    protected bool previousActive = true;

    protected InventoryUI localInventoryUI;
    protected InventoryUI globalInventoryUI;

    protected int allieIndex = 0;
    protected IActor allie;
    protected IStats stats;
    protected IEquipment previousItem;
    protected IEquipment currentItem;
    protected IInventory globalInventory;
    protected ItemTypeBase currentType;

    protected override void OnEnable()
    {
        base.OnEnable();

        globalInventoryUI = new InventoryUI();

        helmetsTab.onClick.RemoveAllListeners();
        meleeWeapons1HTab.onClick.RemoveAllListeners();
        meleeWeapons2HTab.onClick.RemoveAllListeners();
        armorTab.onClick.RemoveAllListeners();
        shieldsTab.onClick.RemoveAllListeners();
        bowsTab.onClick.RemoveAllListeners();
        scrollsTab.onClick.RemoveAllListeners();
        basicTab.onClick.RemoveAllListeners();
        questItemsTab.onClick.RemoveAllListeners();

        helmetsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.helmetType));
        meleeWeapons1HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee1HType));
        meleeWeapons2HTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.melee2HType));
        armorTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.armorType));
        shieldsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.shieldType));
        bowsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.bowType));
        scrollsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.scrollType));
        basicTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.bowType));
        questItemsTab.onClick.AddListener(() => RefreshEquipmentWithSFX(InventoryManager.Instance.questItemType));

        helmetsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.helmetType);
        meleeWeapons1HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.melee1HType);
        meleeWeapons2HTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.melee2HType);
        armorTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.armorType);
        shieldsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.shieldType);
        bowsTab.GetComponent<PointerHover>().onPointerRightClick = () => OnUnequipEquipment(InventoryManager.Instance.bowType);

        slots.Clear();
        slots.Add(InventoryManager.Instance.helmetType, helmetSlot);
        slots.Add(InventoryManager.Instance.melee1HType, meleeWeapon1HSlot);
        slots.Add(InventoryManager.Instance.melee2HType, meleeWeapon2HSlot);
        slots.Add(InventoryManager.Instance.armorType, armorSlot);
        slots.Add(InventoryManager.Instance.shieldType, shieldSlot);
        slots.Add(InventoryManager.Instance.bowType, bowsSlot);

        allieIndex = 0;
        RefreshEquipment(InventoryManager.Instance.helmetType);
        RefreshAllieGeneric();

        AudioManager.Instance.PlaySFX(open);
        GameStateManager.Instance.Pause();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected virtual void RefreshAllieGeneric(int offset = 0)
    {
        allie?.obj.SetActive(previousActive);

        allieIndex += offset;
        allieIndex = Mathf.Clamp(allieIndex, 0, AllieManager.Instance.count - 1);

        allie = AllieManager.Instance[allieIndex];
        previousActive = allie.obj.activeSelf;
        allie.obj.SetActive(true);

        detailedActorViewCamera.cullingMask = (1 << allie.obj.transform.GetChild(0).gameObject.layer)
            | (1 << LayerMask.NameToLayer("Light"));

        stats = allie.getStats;

        allieName.text = allie.obj.name;
        allieStats.text = "";
        allieValues.text = "";
    }

    protected void RefreshLocalScrolls(IInventory inventory)
    {
        localInventoryUI.showName = true;
        localInventoryUI.showCount = false;
        localInventoryUI.showSprite = true;
        localInventoryUI.grid = allieInventoryGrid;
        localInventoryUI.buttonPrefab = buttonPrefab;
        localInventoryUI.inventory = inventory;
        //localInventoryUI.OnClick = ;
        //localInventoryUI.onPointerEnter = OnEnterAllieItem;
        //localInventoryUI.onPointerExit = OnExitAllieItem;
        localInventoryUI.Display();
    }


    protected void RefreshGlobalEquipment(IInventory inventory)
    {
        globalInventoryUI.grid = globalInventoryGrid;
        globalInventoryUI.buttonPrefab = buttonPrefab;
        globalInventoryUI.inventory = inventory;
        globalInventoryUI.OnClick = OnEquipEquipment;
        globalInventoryUI.onPointerEnter = OnEnterGlobalEquipment;
        globalInventoryUI.onPointerExit = OnExitGlobalItem;
        globalInventoryUI.Display();
    }

    protected void RefreshGlobalScrolls(IInventory inventory)
    {
        globalInventoryUI.grid = globalInventoryGrid;
        globalInventoryUI.buttonPrefab = buttonPrefab;
        globalInventoryUI.inventory = inventory;
        globalInventoryUI.OnClick = OnEquipEquipment;
        globalInventoryUI.onPointerEnter = OnEnterGlobalEquipment;
        globalInventoryUI.onPointerExit = OnExitGlobalItem;
        globalInventoryUI.Display();
    }



    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshAllieEquipment(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshAllieEquipment(1);
        }

        detailedActorViewCamera.transform.position = allie.obj.transform.position + new Vector3(0f, 1f, -2.5f);
    }

    protected virtual void RefreshScrollsWithSFX()
    {
        AudioManager.Instance.PlaySFX(cycleEquipmentParts);
        RefreshScrolls();
    }

    protected virtual void RefreshScrolls()
    {

    }

    protected virtual void RefreshBasicWithSFX(ItemTypeBase type)
    {
        AudioManager.Instance.PlaySFX(cycleEquipmentParts);
        RefreshBasic(type);
    }

    protected virtual void RefreshBasic(ItemTypeBase type)
    {
        currentType = type;
        RefreshGlobalEquipment(InventoryManager.Instance.Get(type));
    }

    protected virtual void RefreshQuestItemsSFX(ItemTypeBase type)
    {
        AudioManager.Instance.PlaySFX(cycleEquipmentParts);
        RefreshQuestItems(type);
    }

    protected virtual void RefreshQuestItems(ItemTypeBase type)
    {
        currentType = type;
        RefreshGlobalEquipment(InventoryManager.Instance.Get(type));
    }

    protected virtual void RefreshEquipmentWithSFX(ItemTypeBase part)
    {
        AudioManager.Instance.PlaySFX(cycleEquipmentParts);
        RefreshEquipment(part);
    }

    protected virtual void RefreshEquipment(ItemTypeBase type)
    {
        currentType = type;
        RefreshGlobalEquipment(InventoryManager.Instance.Get(type));
    }

    protected virtual void RefreshAllieEquipment(int offset = 0)
    {
        globalInventory = allie.getEquipment;

        helmetSlot.sprite = helmetSprite;
        meleeWeapon1HSlot.sprite = meleeWeapon1HSprite;
        meleeWeapon2HSlot.sprite = meleeWeapon2HSprite;
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

    protected void OnUnequipEquipment(ItemTypeBase type)
    {
        string itemName = globalInventory.Find(i => ItemDatabase.Instance.GetTypeName(i) == type.name);

        if (itemName == null)
            return;

        currentItem = (IEquipment)ItemDatabase.Instance.Get(itemName);

        InventoryManager.Instance.Get(currentItem.itemType).Add(itemName);
        currentItem.Unequip(allie);
        AudioManager.Instance.PlaySFX(unequip);

        RefreshAllieGeneric();
        RefreshEquipment(currentItem.itemType);
        ClearItemAndAllieData();
    }

    protected void OnEquipEquipment(string itemName)
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

        RefreshAllieGeneric();
        RefreshEquipment(currentType);
        ClearItemAndAllieData();
    }

    
    protected virtual void OnEnterGlobalEquipment(string itemName)
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


    protected void OnExitGlobalItem(string itemName)
    {
        ClearItemAndAllieData();
    }

    protected void ClearItemAndAllieData()
    {
        itemName.text = "";
        itemInfo.text = "";
        allieIncreases.text = "";
        itemSprite.sprite = emptySprite;
    }
}

