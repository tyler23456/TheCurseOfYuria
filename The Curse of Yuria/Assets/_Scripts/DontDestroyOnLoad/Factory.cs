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
        [SerializeField] AssetLabelReference statusEffectsReference;
        [SerializeField] AssetLabelReference partyMemberReference;

        [SerializeField] GameObject damagePopupPrefab;
        [SerializeField] GameObject recoveryPopupPrefab;

        Dictionary<string, IItem> items = new Dictionary<string, IItem>();
        Dictionary<string, IStatusEffect> statusEffects = new Dictionary<string, IStatusEffect>();
        Dictionary<string, GameObject> partyMembers = new Dictionary<string, GameObject>();

        public GameObject getDamagePopupPrefab => damagePopupPrefab;
        public GameObject getRecoveryPopupPrefab => recoveryPopupPrefab;

        private void Awake()
        {
            Addressables.LoadAssetsAsync<IItem>(itemsReference, (i) =>
            {
                items.Add(i.name, i);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<IStatusEffect>(statusEffectsReference, (i) =>
            {
                statusEffects.Add(i.name, i);
            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<GameObject>(partyMemberReference, (i) =>
            {
                partyMembers.Add(i.name, i);
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

        public IStatusEffect GetStatusEffect(string name)
        {
            return statusEffects[name];
        }

        public GameObject GetAllie(string partyMemberName)
        {
            return partyMembers[partyMemberName];
        }
    }
}