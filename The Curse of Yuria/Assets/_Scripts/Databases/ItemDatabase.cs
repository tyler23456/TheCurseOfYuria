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

    Dictionary<string, IItem> items = new Dictionary<string, IItem>();
  
    void Awake()
    {
        Instance = this;
        Populate();
    }

    void Populate()
    {
        Addressables.LoadAssetsAsync<IItem>(itemsReference, (i) =>
        {
            items.Add(i.name, i);
        }).WaitForCompletion();
    }

    public bool Contains(string itemName)
    {
        return items.ContainsKey(itemName);
    }

    public IItem Get(string itemName)
    {
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
}
