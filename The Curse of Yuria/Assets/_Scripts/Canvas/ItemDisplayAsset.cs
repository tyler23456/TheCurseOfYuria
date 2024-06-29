using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.ObjectModel;
using System;

public class ItemDisplayAsset : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] public Button buttonPrefab;
    [SerializeField] public Sprite emptySprite;

    [Header("SFX")]
    [SerializeField] public AudioClip open; // will eventually become scriptable objects
    [SerializeField] public AudioClip close;
    [SerializeField] public AudioClip equip;
    [SerializeField] public AudioClip unequip;
    [SerializeField] public AudioClip cyclePartyMembers;
    [SerializeField] public AudioClip cycleEquipmentParts;

    [Header("Camera And Grids")]
    [SerializeField] public Camera detailedActorViewCamera;
    [SerializeField] public RectTransform globalInventoryGrid;
    [SerializeField] public RectTransform localInventoryGrid;

    [Header("Local Inventory")]
    [SerializeField] public GameObject localInventoryGameObject;

    [Header("Allie Info")]
    [SerializeField] public Text allieName;
    [SerializeField] public Text allieAttribute;
    [SerializeField] public Text allieValue;
    [SerializeField] public Text allieOffset;

    [Header("Item Info")]
    [SerializeField] public Text itemName;
    [SerializeField] public Image itemSprite;
    [SerializeField] public Text itemInfo;

    [Header("Tabs")]
    [SerializeField] public Button helmetsTab;
    [SerializeField] public Button meleeWeapons1HTab;
    [SerializeField] public Button meleeWeapons2HTab;
    [SerializeField] public Button armorTab;
    [SerializeField] public Button shieldsTab;
    [SerializeField] public Button bowsTab;
    [SerializeField] public Button scrollsTab;
    [SerializeField] public Button basicTab;
    [SerializeField] public Button questItemsTab;

    [Header("Slots")]
    [SerializeField] public Image helmetSlot;
    [SerializeField] public Image meleeWeapon1HSlot;
    [SerializeField] public Image meleeWeapon2HSlot;
    [SerializeField] public Image armorSlot;
    [SerializeField] public Image shieldSlot;
    [SerializeField] public Image bowsSlot;

    [Header("Sprites")]
    [SerializeField] public Sprite helmetSprite;
    [SerializeField] public Sprite meleeWeapon1HSprite;
    [SerializeField] public Sprite meleeWeapon2HSprite;
    [SerializeField] public Sprite armorSprite;
    [SerializeField] public Sprite shieldSprite;
    [SerializeField] public Sprite bowsSprite;
    
    InventoryUI localInventoryUI;
    InventoryUI globalInventoryUI;

    [NonSerialized] public bool previousActive = true;
    [NonSerialized] public bool isRefreshingStatusAttributes = true;
    [NonSerialized] public int allieIndex = 0;
    public IActor allie;

    ReadOnlyCollection<Modifier> oldModifiers;
    ReadOnlyCollection<Modifier> newModifiers;
    Modifier oldModifier;
    Modifier newModifier;
    int oldModifierValue = 0;
    int newModifierValue = 0;
    int modifierValue = 0;

    public Action<string> onGlobalClick = (itemName) => { };
    public Action<string> onLocalClick = (itemName) => { };
    public Action<string> onEnterItem = (itemName) => { };
    public Action<string> onExitItem = (itemName) => { };

    public void Initialize()
    {
        globalInventoryUI = new InventoryUI();
        localInventoryUI = new InventoryUI();

        ClearTabListenters();

        allieIndex = 0;
        isRefreshingStatusAttributes = true;
      
        AudioManager.Instance.PlaySFX(open);
        GameStateManager.Instance.Pause();
    }

    public void ClearTabListenters()
    {
        helmetsTab.onClick.RemoveAllListeners();
        meleeWeapons1HTab.onClick.RemoveAllListeners();
        meleeWeapons2HTab.onClick.RemoveAllListeners();
        armorTab.onClick.RemoveAllListeners();
        shieldsTab.onClick.RemoveAllListeners();
        bowsTab.onClick.RemoveAllListeners();
        scrollsTab.onClick.RemoveAllListeners();
        basicTab.onClick.RemoveAllListeners();
        questItemsTab.onClick.RemoveAllListeners();
    }

    public void RefreshAllie(int offset = 0)
    {
        allie?.obj.SetActive(previousActive);

        allieIndex += offset;
        allieIndex = Mathf.Clamp(allieIndex, 0, AllieManager.Instance.count - 1);

        allie = AllieManager.Instance[allieIndex];
        previousActive = allie.obj.activeSelf;
        allie.obj.SetActive(true);

        detailedActorViewCamera.cullingMask = (1 << allie.obj.transform.GetChild(0).gameObject.layer)
            | (1 << LayerMask.NameToLayer("Light"));

        allieName.text = allie.obj.name;

        ClearAllieInfo();
        RefreshTabs();
        RefreshCurrency(0);
        RefreshAllieInfo();
    }

    public void SetLocalInventoryBehavior(bool showName = false, bool showCount = true, bool showSprite = true)
    {
        localInventoryUI.showName = showName;
        localInventoryUI.showCount = showCount;
        localInventoryUI.showSprite = showSprite;
    }

    public void RefreshLocalInventory(IInventory inventory)
    {
        localInventoryUI.grid = localInventoryGrid;
        localInventoryUI.buttonPrefab = buttonPrefab;
        localInventoryUI.inventory = inventory;
        localInventoryUI.OnClick = onLocalClick;
        localInventoryUI.onPointerEnter = onEnterItem;
        localInventoryUI.onPointerExit = onExitItem;
        localInventoryUI.Display();
    }

    public void SetGlobalInventoryBehavior(bool showName = false, bool showCount = true, bool showSprite = true)
    {
        globalInventoryUI.showName = showName;
        globalInventoryUI.showCount = showCount;
        globalInventoryUI.showSprite = showSprite;
    }
    public void RefreshGlobalInventory(IInventory inventory)
    {
        globalInventoryUI.grid = globalInventoryGrid;
        globalInventoryUI.buttonPrefab = buttonPrefab;
        globalInventoryUI.inventory = inventory;
        globalInventoryUI.OnClick = onGlobalClick;
        globalInventoryUI.onPointerEnter = onEnterItem;
        globalInventoryUI.onPointerExit = onExitItem;
        globalInventoryUI.Display();
    }

    public void RefreshItemInfo(ItemTypeBase type)
    {
        ClearAllieAndItemInfo();
        RefreshGlobalInventory(InventoryManager.Instance.Get(type));
        RefreshLocalInventory(allie.getScrolls);
        RefreshTabs();
        RefreshCurrency(0);
        RefreshAllieInfo();
    }

    public void RefreshItemInfo(ItemTypeBase type, Inventory inventory)
    {
        ClearAllieAndItemInfo();
        RefreshGlobalInventory(inventory);
        RefreshLocalInventory(allie.getScrolls);
        RefreshTabs();
        RefreshCurrency(0);
        RefreshAllieInfo();
    }

    public void UpdateAllieView()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshAllie(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshAllie(1);
        }
        detailedActorViewCamera.transform.position = allie.obj.transform.position + new Vector3(0f, 1f, -2.5f);
    }

    public void ShowItemInfo(string itemName)
    {
        IItem current = ItemDatabase.Instance.Get(itemName);

        this.itemName.text = itemName;
        this.itemInfo.text = current.getInfo;
        this.itemSprite.sprite = current.icon;
    }

    public void ClearAllieAndItemInfo(string itemName = "")
    {
        ClearAllieInfo();
        ClearItemInfo();
    }

    public void ClearAllieInfo()
    {
        allieAttribute.text = "";
        allieValue.text = "";
        allieOffset.text = "";
    }

    public void ClearItemInfo()
    {
        this.itemName.text = "";
        this.itemInfo.text = "";
        this.itemSprite.sprite = emptySprite;
    }

    public void AddAllieInfo(string attribute, int value, int offset)
    {
        allieAttribute.text += attribute + "\n";
        allieValue.text += value.ToString() + "\n";
        allieOffset.text += GetValue(offset) + "\n";
    }

    public void AddNewLineToAllieInfo()
    {
        allieAttribute.text += "\n";
        allieValue.text += "\n";
        allieOffset.text += "\n";
    }

    public void RefreshTabs()
    {
        helmetSlot.sprite = helmetSprite;
        meleeWeapon1HSlot.sprite = meleeWeapon1HSprite;
        meleeWeapon2HSlot.sprite = meleeWeapon2HSprite;
        armorSlot.sprite = armorSprite;
        shieldSlot.sprite = shieldSprite;
        bowsSlot.sprite = bowsSprite;

        ItemTypeBase itemType = null;

        Dictionary<ItemTypeBase, Image> slots = new Dictionary<ItemTypeBase, Image>();

        slots.Clear();
        slots.Add(InventoryManager.Instance.helmetType, helmetSlot);
        slots.Add(InventoryManager.Instance.melee1HType, meleeWeapon1HSlot);
        slots.Add(InventoryManager.Instance.melee2HType, meleeWeapon2HSlot);
        slots.Add(InventoryManager.Instance.armorType, armorSlot);
        slots.Add(InventoryManager.Instance.shieldType, shieldSlot);
        slots.Add(InventoryManager.Instance.bowType, bowsSlot);

        for (int i = 0; i < allie.getEquipment.count; i++)
        {
            itemType = ItemDatabase.Instance.GetType(allie.getEquipment.GetName(i));

            slots[itemType].sprite = ItemDatabase.Instance.GetIcon(allie.getEquipment.GetName(i));
            string item = allie.getEquipment.Find(i => ItemDatabase.Instance.GetTypeName(i) == itemType.name);
            if (item != null)
            {
                slots[itemType].GetComponent<PointerHover>().onPointerEnter = () => { ShowItemInfo(item); RefreshAllieInfo(item); };
                slots[itemType].GetComponent<PointerHover>().onPointerExit = () => { ClearItemInfo(); RefreshAllieInfo(""); };
            }
            else
            {
                slots[itemType].GetComponent<PointerHover>().onPointerEnter = () => {  };
                slots[itemType].GetComponent<PointerHover>().onPointerExit = () => {  };
            }
                
        }
    }

    public void RefreshCurrency(int offset = 0)
    {
        AddAllieInfo("Olms", InventoryManager.Instance.olms, offset);
    }

    public void RefreshAllieInfo(string itemName = "")
    {
        RefreshAllieInfo(itemName, 0);
    }

    public void RefreshAllieInfo(string itemName, int currencyOffset)
    {
        ClearAllieInfo();
        AddAllieInfo("Olms", InventoryManager.Instance.olms, currencyOffset);

        AddNewLineToAllieInfo();

        AddAllieInfo("HP", allie.getStats.HP, 0);
        AddAllieInfo("MP", allie.getStats.MP, 0);

        AddNewLineToAllieInfo();

        //if (localInventoryGameObject.activeSelf)
            //RefreshLocalInventory(allie.getScrolls);

        if (!isRefreshingStatusAttributes)
            return;
        
        IEquipment previous = null;
        IEquipment current = null;

        if (ItemDatabase.Instance.Contains(itemName))
            current = (IEquipment)ItemDatabase.Instance.Get(itemName);
        else
            current = (IEquipment)ItemDatabase.Instance.Get("Empty");

        string previousItemName = allie.getEquipment.Find(i => ItemDatabase.Instance.GetTypeName(i) == current.itemType.name);

        if (previousItemName == null)
            previous = (IEquipment)ItemDatabase.Instance.Get("Empty");
        else
            previous = (IEquipment)ItemDatabase.Instance.Get(previousItemName);

        oldModifiers = previous.getModifiers;
        newModifiers = current.getModifiers;

        int[] attributeValues = allie.getStats.GetAttributes();

        string[] attributes = Enum.GetNames(typeof(IStats.Attribute));
        string[] elements = Enum.GetNames(typeof(IStats.Elements));

        for (int i = 0; i < attributes.Length; i++)
        {
            oldModifier = oldModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);
            newModifier = newModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);

            oldModifierValue = oldModifier == null ? 0 : oldModifier.getOffset;
            newModifierValue = newModifier == null ? 0 : newModifier.getOffset;

            modifierValue = newModifierValue - oldModifierValue;

            AddAllieInfo(attributes[i], attributeValues[i], modifierValue);
        }

        AddNewLineToAllieInfo();

        for (int i = 0; i < elements.Length; i++)
        {
            AddAllieInfo(elements[i], allie.getStats.GetWeakness(i), 0);
        }

        AddNewLineToAllieInfo();
    }

    public void RefreshCountersAndInterrupts(string itemName)
    {
        IEquipment current = (IEquipment)ItemDatabase.Instance.Get(itemName);
    }

    string GetValue(int value)
    {
        if (value == 0)
            return "<color=#555555ff>" + value.ToString() + "</color>";
        else if (value > 0)
            return "<color=#00ff00ff>" + "+" + value.ToString() + "</color>";
        else
            return "<color=#ff0000ff>" +  value.ToString() + "</color>";
    }
}

