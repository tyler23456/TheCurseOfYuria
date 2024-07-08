using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using HeroEditor.Common.Enums;

[ExecuteAlways]
public class ItemDatabase : MonoBehaviour//, ISerializationCallbackReceiver
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField] AssetLabelReference itemsReference;
    [SerializeField] bool populate = false;
    [SerializeField] List<IItem> serializedItems = new List<IItem>();

    Dictionary<string, IItem> items = new Dictionary<string, IItem>();

    public List<string> keys = new List<string>();
    public List<IItem> values = new List<IItem>();

    void Awake()
    {
        Instance = this;

        Populate();
    }
    
    void Update()
    {
        if (!populate)
            return;

        populate = false;

        Populate();
    }

    void Populate()
    {
        Addressables.LoadAssetsAsync<IItem>(itemsReference, (i) =>
        {
            //serializedItems.Add(i);
            items.Add(i.name, i);
            
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

    /*public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var item in items)
        {
            keys.Add(item.Key);
            values.Add(item.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        items = new Dictionary<string, IItem>();

        for (int i = 0; i != Mathf.Min(keys.Count, values.Count); i++)
            items.Add(keys[i], values[i]);
    }

    void OnGUI()
    {
        foreach (var kvp in items)
            GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
    }*/
}
