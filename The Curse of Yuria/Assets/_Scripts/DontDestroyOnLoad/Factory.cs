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

        [SerializeField] GameObject damageTextPrefab;
        [SerializeField] GameObject recoveryTextPrefab;

        Dictionary<string, IItem> items = new Dictionary<string, IItem>();

        public GameObject getDamageTextPrefab => damageTextPrefab;
        public GameObject getRecoveryTextPrefab => recoveryTextPrefab;

        private void Awake()
        {
            Addressables.LoadAssetsAsync<IItem>(itemsReference, (i) =>
            {
                items.Add(i.itemName, i);
            }).WaitForCompletion();
        }

        public IItem GetItem(string name)
        {
            return items[name];
        }

        public bool HasItem(string name)
        {
            return items.ContainsKey(name);
        }
    }
}