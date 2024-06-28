using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using HeroEditor.Common.Enums;

[ExecuteAlways]
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField] AssetLabelReference itemsReference;
    [SerializeField] bool populate = false;
    [SerializeField] List<ItemBase> serializedItems = new List<ItemBase>();

    Dictionary<string, IItem> items = new Dictionary<string, IItem>();

    void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        if (!populate)
            return;

        populate = false;

        Addressables.LoadAssetsAsync<ItemBase>(itemsReference, (i) =>
        {
            serializedItems.Add(i);
        }).WaitForCompletion();
    }

    public bool Contains(string itemName)
    {
        CheckForEmptyDictionary();
        return items.ContainsKey(itemName);
    }

    public IItem Get(string itemName)
    {
        CheckForEmptyDictionary();
        return items[itemName];
    }

    public ItemTypeBase GetType(string itemName)
    {
        return Get(itemName).itemType;
    }

    public Sprite GetIcon(string itemName)
    {
        return Get(itemName).icon;
    }

    public string GetTypeName(string itemName)
    {
        return Get(itemName).itemType.name;
    }

    public EquipmentPart GetPart(string itemName)
    {
        return Get(itemName).itemType.part;
    }

    void CheckForEmptyDictionary()
    {
        if (items.Count == 0)
        {
            foreach (IItem item in serializedItems)
                items.Add(item.name, item);
            serializedItems.Clear();
        }
    }
}
