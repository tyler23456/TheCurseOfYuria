using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;
using UnityEngine.AddressableAssets;

namespace TCOY.DontDestroyOnLoad
{
    public class Factory : MonoBehaviour, IFactory
    {
        [SerializeField] SpriteCollection spriteCollection;

        [SerializeField] AssetLabelReference itemsReference;
        [SerializeField] AssetLabelReference particleSystemReference;
        [SerializeField] AssetLabelReference abilitiesReference;
        [SerializeField] AssetLabelReference equipmentReference;

        public Dictionary<string, ItemSprite> itemSprites { get; private set; } = new Dictionary<string, ItemSprite>();
        public Dictionary<string, IInteractable> itemPrefabs { get; private set; } = new Dictionary<string, IInteractable>();
        public Dictionary<string, GameObject> particleSystemPrefabs { get; private set; } = new Dictionary<string, GameObject>();
        public Dictionary<string, IAbility> abilityPrefabs { get; private set; } = new Dictionary<string, IAbility>();
        public Dictionary<string, IEquipable> equipmentPrefabs { get; private set; } = new Dictionary<string, IEquipable>();

        public SpriteCollection getSpriteCollection => spriteCollection;

        private void Awake()
        {
            List<ItemSprite> items = getSpriteCollection.GetAllSprites();

            foreach (ItemSprite item in items)
                itemSprites.Add(item.Name, item);

            Addressables.LoadAssetsAsync<GameObject>(itemsReference, (i) =>
            {
                itemPrefabs.Add(i.name, i.GetComponent<IInteractable>());
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<GameObject>(particleSystemReference, (i) =>
            {
                particleSystemPrefabs.Add(i.name, i);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<GameObject>(abilitiesReference, (i) =>
            {
                abilityPrefabs.Add(i.name, i.GetComponent<IAbility>());
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<GameObject>(equipmentReference, (i) =>
            {
                equipmentPrefabs.Add(i.name, i.GetComponent<IEquipable>());
            }).WaitForCompletion();
        }

        public IEnemy GetEnemyPrefab(string enemyPrefabName)
        {
            return null;
        }

        public EquipmentPart GetEquipmentPart(string itemName)
        {
            ItemSprite item = itemSprites[itemName];
            string partString = item.Id.Split('.')[2];
            System.Enum.TryParse(partString, out EquipmentPart result);
            return result;
        }
    }
}