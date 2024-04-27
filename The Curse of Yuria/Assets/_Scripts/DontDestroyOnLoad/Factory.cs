using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;
using UnityEngine.AddressableAssets;
using Assets.HeroEditor.Common.Scripts.Collections;
using Assets.HeroEditor.Common.Scripts.Data;

namespace TCOY.DontDestroyOnLoad
{
    public class Factory : MonoBehaviour, IFactory
    {
        [SerializeField] AssetLabelReference itemsReference;
        [SerializeField] AssetLabelReference particleSystemReference;
        [SerializeField] AssetLabelReference abilitiesReference;

        Dictionary<string, ItemIcon> icons = new Dictionary<string, ItemIcon>();
        Dictionary<string, ItemSprite> sprites = new Dictionary<string, ItemSprite>();

        Dictionary<string, Sprite> itemPrefabs = new Dictionary<string, Sprite>();     
        Dictionary<string, GameObject> particleSystemPrefabs = new Dictionary<string, GameObject>();
        Dictionary<string, ISkill> abilityPrefabs = new Dictionary<string, ISkill>();

        private void Awake()
        {


            Addressables.LoadAssetsAsync<Sprite>(itemsReference, (i) =>
            {
                itemPrefabs.Add(i.name, i);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<GameObject>(particleSystemReference, (i) =>
            {
                particleSystemPrefabs.Add(i.name, i);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<GameObject>(abilitiesReference, (i) =>
            {
                abilityPrefabs.Add(i.name, i.GetComponent<ISkill>());
            }).WaitForCompletion();
        }

        public EquipmentPart GetEquipmentPart(string itemName)
        {
            ItemSprite item = sprites[itemName];
            string partString = item.Id.Split('.')[2];
            System.Enum.TryParse(partString, out EquipmentPart result);
            return result;
        }

        public ItemSprite GetSprite(string name)
        {
            return sprites[name];
        }

        public ItemIcon GetIcon(string name)
        {
            return icons[name];
        }

        public Sprite GetItemPrefab(string name)
        {
            return itemPrefabs[name];
        }

        public GameObject GetParticleSystemPrefab(string name)
        {
            return particleSystemPrefabs[name];
        }

        public ISkill GetAbilityPrefab(string name)
        {
            return abilityPrefabs[name];
        }
    }
}