using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.Interactables
{
    public class RandomDrop : InteractableBase, IInteractablePointer, IEnabler
    {
        [Range(0, 20)]public int minCount = 1;
        [Range(1, 20)]public int maxCount = 3;

        [SerializeField] public List<WeightedEntry> weightedEntries;

        List<IItem> items = new List<IItem>();
        List<float> weights = new List<float>();
        int count = 1;

        IItem item;
        Inventory inventory = new Inventory();

        bool isFirstEnable = true;
        
        protected void OnValidate()
        {
            if (minCount > maxCount)
                minCount = maxCount;
        }

        public void Reset()
        {
            weightedEntries = new List<WeightedEntry>();
        }

        void OnEnable()
        {
            if (!isFirstEnable)
                return;

            isFirstEnable = true;

            if (weightedEntries.Count == 0)
                return;

            foreach (WeightedEntry entry in weightedEntries)
            {
                items.Add(entry.item);
                weights.Add(entry.weight);
            }

            count = Random.Range(minCount, maxCount);
            count = Mathf.Clamp(count, 1, weightedEntries.Count);

            for (int i = 0; i < count; i++)
            {
                WeightedEntry weightedEntry = new WeightedEntry();
                IItem item = WeightedDecision.Generate(items, weights);
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
        public IItem item;
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