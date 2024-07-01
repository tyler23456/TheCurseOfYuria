using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.DontDestroyOnLoad
{
    public class RandomDrop : InteractableBase, IInteractablePointer, IEnabler
    {
        [Range(0, 20)]public int minCount = 1;
        [Range(1, 20)]public int maxCount = 3;

        [SerializeField] public List<WeightedEntry> weightedEntries;

        List<ItemBase> items = new List<ItemBase>();
        List<float> weights = new List<float>();
        int count = 1;
        
        void OnValidate()
        {
            if (minCount > maxCount)
                minCount = maxCount;

            //foreach (WeightedEntry entry in weightedEntries)
                //if (entry.minCount > entry.maxCount)
                    //entry.minCount = entry.maxCount;
        }

        void OnEnable()
        {
            foreach (WeightedEntry entry in weightedEntries)
            {
                items.Add(entry.item);
                weights.Add(entry.weight);
            }

            count = Random.Range(minCount, maxCount);
        }

        public override void Interact(IActor player)
        {
            for (int i = 0; i < count; i++)
            {
                WeightedEntry weightedEntry = new WeightedEntry();
                weightedEntry = WeightedDecision.Generate(weightedEntries, weights);
                int entryCount = Random.Range(weightedEntry.minCount, weightedEntry.maxCount);

                InventoryManager.Instance.AddItem(weightedEntry.item.name, entryCount);
                ObtainedItemsDisplay.Instance.getInventory.Add(weightedEntry.item.name, entryCount);
            }
            Transform obtainedItemsDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/ObtainedItemsDisplay").transform;
            obtainedItemsDisplay.gameObject.SetActive(false);
            obtainedItemsDisplay.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class WeightedEntry
    {
        public ItemBase item;
        [Range(1, 20)] public int minCount;
        [Range(1, 20)] public int maxCount;
        public float weight;
    }

    public static class WeightedDecision
    {
        static float weightSum = 0f;

        public static T Generate<T>(List<T> decisions, List<float> weights)
        {
            weightSum = weights.Sum();

            float randomNumber = UnityEngine.Random.Range(0f, 1f);

            float weightAccumulator = 0;
            float normalizedWeight;

            for (int i = 0; i < decisions.Count; i++)
            {
                normalizedWeight = weights[i] / weightSum;
                weightAccumulator += normalizedWeight;

                if (randomNumber <= weightAccumulator)
                    return decisions[i];
            }
            return decisions.Last();
        }
    }
}