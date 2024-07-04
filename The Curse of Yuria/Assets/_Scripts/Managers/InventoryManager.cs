using System.Collections.Generic;
using UnityEngine;

public sealed class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    Dictionary<string, Inventory> inventories = new Dictionary<string, Inventory>();

    public int olms { get; set; } = 107;
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

    public string helmetType => "Helmet";
    public string melee1HandedType => "Melee1Handed";
    public string melee2HandedType => "Melee2Handed";
    public string armorType => "Armor";
    public string shieldType => "Shield";
    public string bowType => "Bow";
    public string basicType => "Basic";
    public string scrollType => "Scroll";
    public string questItemType => "QuestItem";

    void Awake()
    {
        Instance = this;

        inventories.Add("Helmet", helmets);
        inventories.Add("Melee1Handed", meleeWeapons1H);
        inventories.Add("Melee2Handed", meleeWeapons2H);
        inventories.Add("Armor", armor);
        inventories.Add("Shield", shields);
        inventories.Add("Bow", bows);
        inventories.Add("Basic", basic);
        inventories.Add("Scroll", scrolls);
        inventories.Add("QuestItem", questItems);
    }

    public void AddItem(string itemName, int count = 1)
    {
        inventories[ItemDatabase.Instance.GetType(itemName)].Add(itemName, count);
    }

    public Inventory Get(string type)
    {
        return inventories[type];
    }

    public IInventory GetInventoryOf(string itemName)
    {
        return inventories[ItemDatabase.Instance.Get(itemName).type];
    }

    public void EmptyAllInventories()
    {
        foreach (KeyValuePair<string, Inventory> inventory in inventories)
            inventory.Value.Clear();
    }
}
