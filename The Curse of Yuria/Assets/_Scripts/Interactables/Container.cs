using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Interactables
{
    public class Container : InteractableBase, IInteractable, IInteractablePointer
    {
        [SerializeField] protected SavedEntry[] entries;

        public override string getAction => "Open ";

        protected void OnValidate()
        {
            if (entries != null && entries.Length > 0)
            
            entries[0].Initialize("");
            for (int i = 1; i < entries.Length; i++)
                entries[i].Initialize(entries[i - 1].ID);
        }

        void PlaySoundEffect()
        {
            if (name.Contains("Chest"))
                InteractableSFXManager.Instance.PlayOpenChestSFX(GetComponent<AudioSource>());
            else if (name.Contains("sack"))
                InteractableSFXManager.Instance.PlayOpenSackSFX(GetComponent<AudioSource>());
            else
                InteractableSFXManager.Instance.PlayOpenCrateSFX(GetComponent<AudioSource>());
        }

        public override void Interact(IActor player)
        {
            GameObject obj = GameObject.Find("/DontDestroyOnLoad/Canvas/ObtainedItemsDisplay");
            obj.SetActive(false);

            PlaySoundEffect();

            foreach (SavedEntry entry in entries)
            {
                int count = entry.count - InventoryManager.Instance.completedIds.GetCount(entry.ID);

                if (count <= 0)
                    continue;

                IObtainedItemsData.inventory.Add(entry.item.name, count);
            }

            IObtainedItemsData.onClick = OnClick;

            obj.SetActive(true);
        }

        public void OnClick(string itemName)
        {
            foreach (SavedEntry entry in entries)
                if (entry.item.name == itemName)
                    InventoryManager.Instance.completedIds.Add(entry.ID);
        }
    }
}