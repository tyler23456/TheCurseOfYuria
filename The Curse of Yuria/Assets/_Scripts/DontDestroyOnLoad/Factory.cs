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
        public Dictionary<string, IItem> itemPrefabs { get; private set; } = new Dictionary<string, IItem>();
        public Dictionary<string, GameObject> particleSystemPrefabs { get; private set; } = new Dictionary<string, GameObject>();
        public Dictionary<string, IAbility> abilityPrefabs { get; private set; } = new Dictionary<string, IAbility>();
        public Dictionary<string, IEquipment> equipmentPrefabs { get; private set; } = new Dictionary<string, IEquipment>();

        public SpriteCollection getSpriteCollection => spriteCollection;
        
        private void Awake()
        {
            List<ItemSprite> items = getSpriteCollection.GetAllSprites();

            foreach (ItemSprite item in items)
                itemSprites.Add(item.Name, item);

            Addressables.LoadAssetsAsync<GameObject>(itemsReference, (i) =>
            {
                itemPrefabs.Add(i.name, i.GetComponent<IItem>());
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
                equipmentPrefabs.Add(i.name, i.GetComponent<IEquipment>());
            }).WaitForCompletion();
        }

        public IEnemy GetEnemyPrefab(string enemyPrefabName)
        {
            return null;
        }
    }
}