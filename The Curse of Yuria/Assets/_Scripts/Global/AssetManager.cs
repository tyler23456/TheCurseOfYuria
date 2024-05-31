using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System;
using HeroEditor.Common;
using System.Linq;

namespace TCOY.DontDestroyOnLoad
{
    [ExecuteInEditMode]
    public class AssetManager : MonoBehaviour
    {
        [SerializeField] Material material;

        [SerializeField] NoArmType noArm;
        [SerializeField] MeleeType melee;
        [SerializeField] RangedType ranged;
        [SerializeField] MagicType magic;
        [SerializeField] SupplyType supply;

        [SerializeField] NoElementType noElement;

        [SerializeField] DamageType damage;
        [SerializeField] NoCalculationType neutral;

        [SerializeField] string iconsRootPath = "Icons/Equipment/";
        [SerializeField] string spriteRootPath = "Sprites/Equipment/";

        [SerializeField] string helmetsPath = "Helmet/";
        [SerializeField] string earringsPath = "Earrings/";
        [SerializeField] string glassesPath = "Glasses/";
        [SerializeField] string masksPath = "Mask/";
        [SerializeField] string meleeWeapon1HPath = "MeleeWeapon1H/";
        [SerializeField] string meleeWeapon2HPath = "MeleeWeapon2H/";
        [SerializeField] string capesPath = "Cape/";
        [SerializeField] string armorPath = "Armor/";
        [SerializeField] string shieldsPath = "Shield/";
        [SerializeField] string bowsPath = "Bow/";
        [SerializeField] string scrollsPath = "Supplies/Scroll/";
        [SerializeField] string suppliesPath = "Supplies/Basic/";
        [SerializeField] string gemPath = "Supplies/Gem/";
        [SerializeField] string questItemPath = "Supplies/QuestItem/";

        [SerializeField] string assetsRootPath = "Assets/_Scriptable/Items/";
        [SerializeField] string prefabsRootPath = "Assets/User/Prefabs/Items/";

        [SerializeField] bool refresh;
        [SerializeField] bool createAllWithNoMatchingIcon = true;
        [SerializeField] bool removeAllWithNoMatchingIcon = false;
        [SerializeField] bool refreshPrefabs = false;

        GameObject prefab;
        ItemBase asset;
        GameObject obj;

        new BoxCollider2D collider;
        Item item;
        new SpriteRenderer renderer;

        Dictionary<string, ItemSprite> itemSprites = new Dictionary<string, ItemSprite>();

        void Update()
        {
            if (!refresh)
                return;

            refresh = false;

            SpriteCollection spriteCollection = Resources.Load<SpriteCollection>("SpriteCollection");
            foreach (ItemSprite itemSprite in spriteCollection.GetAllSprites())
            {
                if (!itemSprites.ContainsKey(itemSprite.Name))
                    itemSprites.Add(itemSprite.Name, itemSprite);
                Debug.Log(itemSprite.Name);
            }
            
            if (!createAllWithNoMatchingIcon)
                return;

            Sprite[] helmets = Resources.LoadAll<Sprite>(iconsRootPath + helmetsPath);
            Sprite[] earrings = Resources.LoadAll<Sprite>(iconsRootPath + earringsPath);
            Sprite[] glasses = Resources.LoadAll<Sprite>(iconsRootPath + glassesPath);
            Sprite[] masks = Resources.LoadAll<Sprite>(iconsRootPath + masksPath);
            Sprite[] meleeWeapon1H = Resources.LoadAll<Sprite>(iconsRootPath + meleeWeapon1HPath);
            Sprite[] meleeWeapon2H = Resources.LoadAll<Sprite>(iconsRootPath + meleeWeapon2HPath);
            Sprite[] capes = Resources.LoadAll<Sprite>(iconsRootPath + capesPath);
            Sprite[] armor = Resources.LoadAll<Sprite>(iconsRootPath + armorPath);
            Sprite[] shields = Resources.LoadAll<Sprite>(iconsRootPath + shieldsPath);
            Sprite[] bows = Resources.LoadAll<Sprite>(iconsRootPath + bowsPath);
            Sprite[] scrolls = Resources.LoadAll<Sprite>(iconsRootPath + scrollsPath);
            Sprite[] supplies = Resources.LoadAll<Sprite>(iconsRootPath + suppliesPath);
            Sprite[] gems = Resources.LoadAll<Sprite>(iconsRootPath + gemPath);
            Sprite[] questItems = Resources.LoadAll<Sprite>(iconsRootPath + questItemPath);


            foreach (Sprite icon in helmets)
                RefreshItemCategory(icon, helmetsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.helmetType, noArm, noElement, neutral);

            foreach (Sprite icon in earrings)
                RefreshItemCategory(icon, earringsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.earringType, noArm, noElement, neutral);

            foreach (Sprite icon in glasses)
                RefreshItemCategory(icon, glassesPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.glassesType, noArm, noElement, neutral);

            foreach (Sprite icon in masks)
                RefreshItemCategory(icon, masksPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.maskType, noArm, noElement, neutral);

            foreach (Sprite icon in meleeWeapon1H)
                RefreshItemCategory(icon, meleeWeapon1HPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.melee1HType, melee, noElement, damage);

            foreach (Sprite icon in meleeWeapon2H)
                RefreshItemCategory(icon, meleeWeapon2HPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.melee2HType, melee, noElement, damage);

            foreach (Sprite icon in capes)
                RefreshItemCategory(icon, capesPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.capeType, noArm, noElement, neutral);

            foreach (Sprite icon in armor)
                RefreshItemCategory(icon, armorPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.armorType, noArm, noElement, neutral);

            foreach (Sprite icon in shields)
                RefreshItemCategory(icon, shieldsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.shieldType, noArm, noElement, neutral);

            foreach (Sprite icon in bows)
                RefreshItemCategory(icon, bowsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipable>(), InventoryManager.Instance.bowType, ranged, noElement, damage);

            foreach (Sprite icon in scrolls)
                RefreshItemCategory(icon, scrollsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Scroll>(), InventoryManager.Instance.scrollType, magic, noElement, neutral);

            foreach (Sprite icon in supplies)
                RefreshItemCategory(icon, suppliesPath + FilterName(icon.name), ScriptableObject.CreateInstance<Supply>(), InventoryManager.Instance.basicType, supply, noElement, neutral);

            foreach (Sprite icon in gems)
                RefreshItemCategory(icon, gemPath + FilterName(icon.name), ScriptableObject.CreateInstance<Gem>(), InventoryManager.Instance.gemType, noArm, noElement, neutral);

            foreach (Sprite icon in questItems)
                RefreshItemCategory(icon, questItemPath + FilterName(icon.name), ScriptableObject.CreateInstance<QuestItem>(), InventoryManager.Instance.questItemType, noArm, noElement, neutral);
        }
        
        void RefreshItemCategory(Sprite icon, string path, ItemBase scriptableObject, ItemTypeBase itemType, ArmTypeBase armType, ElementTypeBase elementType, CalculationTypeBase calculationType)
        {
            prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabsRootPath + path + ".prefab", typeof(GameObject));
            asset = (ItemBase)AssetDatabase.LoadAssetAtPath(assetsRootPath + path + ".asset", typeof(ItemBase));

            if (prefab == null)
                prefab = CreateOrReplacePrefab(icon, path);
            else if (refreshPrefabs)
                RefreshPrefab(icon, prefab, path);

            if (asset == null)
                CreateScriptableObject(icon, prefab, scriptableObject, path, itemType, armType, elementType, calculationType);
            else
                RefreshScriptableObject(icon, prefab, path, itemType, armType, elementType, calculationType);
        }

        GameObject CreateOrReplacePrefab(Sprite icon, string path)
        {
            obj = new GameObject(FilterName(icon.name));
            obj.AddComponent<BoxCollider2D>().isTrigger = true;
            obj.AddComponent<Item>();
            renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = icon;
            renderer.sharedMaterial = material;
            renderer.sortingOrder = 200;

            GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(obj, prefabsRootPath + path + ".prefab", InteractionMode.UserAction);
            DestroyImmediate(obj);
            return prefab;
        }

        void RefreshPrefab(Sprite icon, GameObject prefab, string path)
        {
            collider = prefab.GetComponent<BoxCollider2D>();
            item = prefab.GetComponent<Item>();
            renderer = prefab.GetComponent<SpriteRenderer>();

            if (collider != null
                && collider.isTrigger == true
                && item != null
                && renderer != null
                && renderer.sprite == icon
                && renderer.sharedMaterial == material
                && renderer.sortingOrder == 200
                && CompareNames(icon.name, prefab.name))
                return;

            CreateOrReplacePrefab(icon, path);
        }

        void CreateScriptableObject(Sprite icon, GameObject prefab, ItemBase scriptableObject, string path, ItemTypeBase itemType, ArmTypeBase armType, ElementTypeBase elementType, CalculationTypeBase calculationType)
        {
            AssetDatabase.CreateAsset(scriptableObject, assetsRootPath + path + ".asset");
            RefreshScriptableObject(icon, prefab, path, itemType, armType, elementType, calculationType);
        }

        void RefreshScriptableObject(Sprite icon, GameObject prefab, string path, ItemTypeBase itemType, ArmTypeBase armType, ElementTypeBase elementType, CalculationTypeBase calculationType)
        {
            asset = (ItemBase)AssetDatabase.LoadAssetAtPath(assetsRootPath + path + ".asset", typeof(ItemBase));


            if (CompareItemSprite(asset.itemSprite, itemSprites[icon.name])
                && asset.icon == icon
                && asset.prefab == prefab
                && CompareNames(icon.name, asset.name)
                && asset.itemType != null
                && asset.itemType.name == itemType.name
                && asset.armType != null
                && asset.elementType != null
                && asset.calculationType != null)
                return;

            AssetDatabase.RenameAsset(assetsRootPath + path + ".asset", FilterName(icon.name));
            asset.icon = icon;
            asset.itemSprite = itemSprites[icon.name];
            asset.prefab = prefab;
            asset.itemType = itemType;
            asset.armType = armType;
            asset.elementType = elementType;
            asset.calculationType = calculationType;

            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssetIfDirty(asset);
        }

        bool CompareItemSprite(ItemSprite a, ItemSprite b)
        {
            return a.Hash == b.Hash 
                && a.Id == b.Id 
                && a.Multiple == b.Multiple 
                && a.Name == b.Name 
                && a.Path == b.Path
                && a.Sprite == b.Sprite 
                && a.Sprites.SequenceEqual(b.Sprites) 
                && a.Tags.SequenceEqual(b.Tags) 
                && a.Collection == b.Collection
                && a.Edition == b.Edition;
        }

        bool CompareNames(string a, string b)
        {
            return FilterName(a) == b;
        }

        string FilterName(string a)
        {     
            return a.Replace(" [Paint]", "").Replace(" [FullHair]", "");
        }
    }
}