using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

[CreateAssetMenu(fileName = "NewShop", menuName = "Cutscene/Shop")]
public class Shop : ActionBase
{
    const float SellDivisor = 2f;

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
        ShopDisplay.Instance.sellersRating = sellersRating / SellDivisor;
        ShopDisplay.Instance.onBuyItem = OnBuyItem;
        ShopDisplay.Instance.ShowExclusivelyInParent();

        yield return null;
    }

    public void OnBuyItem(string itemName)
    {
        foreach (SavedEntry entry in entries)
            if (entry.item.name == itemName)
                InventoryManager.Instance.completedIds.Add(entry.ID);
    }

    public void OnSellItem(string itemName)
    {
        //checks to see if item exists, if it does not, it creates a new ID.  If it does exist, takes out ID from player ids
    }
}
