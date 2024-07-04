using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using HeroEditor.Common.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[ExecuteAlways]
public class ItemDatabase : SerializedMonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField] AssetLabelReference itemsReference;
    [SerializeField] bool populate = false;
    [OdinSerialize] List<IItem> serializedItems = new List<IItem>();

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

        Addressables.LoadAssetsAsync<IItem>(itemsReference, (i) =>
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

    public string GetType(string itemName)
    {
        return Get(itemName).type;
    }

    public EquipmentPart Part(string itemName)
    {
        return ((IEquipment)Get(itemName)).part;
    }

    public Sprite GetIcon(string itemName)
    {
        return Get(itemName).icon;
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
