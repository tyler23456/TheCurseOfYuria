using System.Collections.Generic;
using UnityEngine;

public sealed class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] HelmetType _helmet;
    [SerializeField] EarringType _earring;
    [SerializeField] GlassesType _glasses;
    [SerializeField] MaskType _mask;
    [SerializeField] Melee1HType _melee1H;
    [SerializeField] Melee2HType _melee2H;
    [SerializeField] CapeType _cape;
    [SerializeField] ArmorType _armor;
    [SerializeField] ShieldType _shield;
    [SerializeField] BowType _bow;
    [SerializeField] BasicType _basic;
    [SerializeField] ScrollType _scroll;
    [SerializeField] GemType _gem;
    [SerializeField] QuestItemType _questItem;

    Dictionary<string, Inventory> inventories = new Dictionary<string, Inventory>();

    public Inventory helmets { get; private set; } = new Inventory();
    public Inventory earrings { get; private set; } = new Inventory();
    public Inventory glasses { get; private set; } = new Inventory();
    public Inventory masks { get; private set; } = new Inventory();
    public Inventory meleeWeapons1H { get; private set; } = new Inventory();
    public Inventory meleeWeapons2H { get; private set; } = new Inventory();
    public Inventory capes { get; private set; } = new Inventory();
    public Inventory armor { get; private set; } = new Inventory();
    public Inventory shields { get; private set; } = new Inventory();
    public Inventory bows { get; private set; } = new Inventory();
    public Inventory scrolls { get; private set; } = new Inventory();
    public Inventory basic { get; private set; } = new Inventory();
    public Inventory questItems { get; private set; } = new Inventory();
    public Inventory completedQuests { get; private set; } = new Inventory();
    public Inventory completedIds { get; private set; } = new Inventory();

    public HelmetType helmetType => _helmet;
    public EarringType earringType => _earring;
    public GlassesType glassesType => _glasses;
    public MaskType maskType => _mask;
    public Melee1HType melee1HType => _melee1H;
    public Melee2HType melee2HType => _melee2H;
    public CapeType capeType => _cape;
    public ArmorType armorType => _armor;
    public ShieldType shieldType => _shield;
    public BowType bowType => _bow;
    public BasicType basicType => _basic;
    public ScrollType scrollType => _scroll;
    public GemType gemType => _gem;
    public QuestItemType questItemType => _questItem;

    void Awake()
    {
        Instance = this;

        inventories.Add(_helmet.name, helmets);
        inventories.Add(_earring.name, earrings);
        inventories.Add(_glasses.name, glasses);
        inventories.Add(_mask.name, masks);
        inventories.Add(_melee1H.name, meleeWeapons1H);
        inventories.Add(_melee2H.name, meleeWeapons2H);
        inventories.Add(_cape.name, capes);
        inventories.Add(_armor.name, armor);
        inventories.Add(_shield.name, shields);
        inventories.Add(_bow.name, bows);
        inventories.Add(_basic.name, basic);
        inventories.Add(_scroll.name, scrolls);
        inventories.Add(_questItem.name, questItems);
    }

    public void AddItem(string itemName, int count = 1)
    {
        inventories[ItemDatabase.Instance.GetTypeName(itemName)].Add(itemName, count);
    }

    public Inventory Get(ItemTypeBase type)
    {
        return inventories[type.name];
    }

    public void EmptyAllInventories()
    {
        foreach (KeyValuePair<string, Inventory> inventory in inventories)
            inventory.Value.Clear();
    }
}
