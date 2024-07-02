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

        List<ItemSO> items = new List<ItemSO>();
        List<float> weights = new List<float>();
        int count = 1;

        ItemSO item;
        Inventory inventory = new Inventory();

        bool isFirstEnable = true;
        
        public new void OnValidate()
        {
            base.OnValidate();

            if (minCount > maxCount)
                minCount = maxCount;
        }

        void OnEnable()
        {
            if (!isFirstEnable)
                return;

            isFirstEnable = true;

            foreach (WeightedEntry entry in weightedEntries)
            {
                items.Add(entry.item);
                weights.Add(entry.weight);
            }

            count = Random.Range(minCount, maxCount);

            for (int i = 0; i < count; i++)
            {
                WeightedEntry weightedEntry = new WeightedEntry();
                ItemSO item = WeightedDecision.Generate(items, weights);
                int entryCount = Random.Range(weightedEntry.minCount, weightedEntry.maxCount);
                inventory.Add(item.name, entryCount);
            }
        }

        public override void Interact(IActor player)
        {
            for (int i = 0; i < IObtainedItemsData.inventory.count; i++)
                IObtainedItemsData.inventory.Add(inventory.GetName(i), inventory.GetCount(i));
                      
            Transform obtainedItemsDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/ObtainedItemsDisplay").transform;
            obtainedItemsDisplay.gameObject.SetActive(false);
            obtainedItemsDisplay.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class WeightedEntry
    {
        public ItemSO item;
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