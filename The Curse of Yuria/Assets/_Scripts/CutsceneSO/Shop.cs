using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

[CreateAssetMenu(fileName = "NewShop", menuName = "Cutscene/Shop")]
public class Shop : ActionBase
{
    [SerializeField] [Range(0.1f, 10f)] float buyersRating = 1f;
    [SerializeField] [Range(0.1f, 10f)] float sellersRating = 1f;
    [SerializeField] List<SavedEntry> entries;

    void OnValidate()
    {
        foreach (SavedEntry entry in entries)
            if (entry != null && entry.ID == "None" || entry.ID == "")
                entry.ID = System.DateTime.Now.Ticks.ToString() + "|" + System.Guid.NewGuid().ToString();
    }

    public override IEnumerator Activate()
    {
        foreach (SavedEntry entry in entries)
        {
            int count = entry.count - InventoryManager.Instance.completedIds.GetCount(entry.ID);

            if (count <= 0)
                continue;

            ObtainedItemsDisplay.Instance.getInventory.Add(entry.item.name, count);
            ShopDisplay.Instance.itemsForSell.Add((entry.item.name, entry.count));
        }

        ShopDisplay.Instance.buyersRating = buyersRating;
        ShopDisplay.Instance.sellersRating = sellersRating;
        ShopDisplay.Instance.onClickGlobalItem = OnClick;
        ShopDisplay.Instance.ShowExclusivelyInParent();

        yield return null;
    }

    public void OnClick(string itemName)
    {
        foreach (SavedEntry entry in entries)
            if (entry.item.name == itemName)
                InventoryManager.Instance.completedIds.Add(entry.ID);
    }
}
