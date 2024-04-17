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

        public Dictionary<string, ItemSprite> itemSprites { get; private set; } = new Dictionary<string, ItemSprite>();
        public Dictionary<string, GameObject> itemPrefabs { get; private set; } = new Dictionary<string, GameObject>();

        public SpriteCollection getSpriteCollection => spriteCollection;
        
        private void Awake()
        {
            List<ItemSprite> items = getSpriteCollection.GetAllSprites();

            foreach (ItemSprite item in items)
                itemSprites.Add(item.Name, item);

            Addressables.LoadAssetsAsync<GameObject>(itemsReference, (i) =>
            {
                itemPrefabs.Add(i.name, i);
            }).WaitForCompletion();
        }

        public IEnemy GetEnemyPrefab(string enemyPrefabName)
        {
            return null;
        }
    }
}