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
        IFactory factory;

        [SerializeField] Material material;

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
        [SerializeField] string scrollsPath = "Scroll/";
        [SerializeField] string suppliesPath = "Supplies/";
        [SerializeField] string gemPath = "Gem/";
        [SerializeField] string questItemPath = "QuestItem/";

        [SerializeField] string assetsRootPath = "Assets/_Scriptable/Items/";
        [SerializeField] string prefabsRootPath = "Assets/User/Prefabs/Items/";

        [SerializeField] bool refresh;
        [SerializeField] bool createAllWithNoMatchingIcon = true;
        [SerializeField] bool removeAllWithNoMatchingIcon = false;
        [SerializeField] bool refreshPrefabs = false;

        GameObject prefab;
        IItem asset;
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
                if (!itemSprites.ContainsKey(itemSprite.Name))
                    itemSprites.Add(itemSprite.Name, itemSprite);

            factory = GetComponent<IFactory>();

            if (!createAllWithNoMatchingIcon)
                return;

            Sprite[] helmets = Resources.LoadAll<Sprite>(iconsRootPath + helmetsPath);
            Sprite[] earrings = Resources.LoadAll<Sprite>(iconsRootPath + earringsPath);
            Sprite[] glasses = Resources.LoadAll<Sprite>(iconsRootPath + glassesPath);
            Sprite[] masks = Resources.LoadAll<Sprite>(iconsRootPath + helmetsPath);
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
                RefreshItemCategory(icon, helmetsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in earrings)
                RefreshItemCategory(icon, earringsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in glasses)
                RefreshItemCategory(icon, glassesPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in masks)
                RefreshItemCategory(icon, masksPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in meleeWeapon1H)
                RefreshItemCategory(icon, meleeWeapon1HPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in meleeWeapon2H)
                RefreshItemCategory(icon, meleeWeapon2HPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in capes)
                RefreshItemCategory(icon, capesPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in armor)
                RefreshItemCategory(icon, armorPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in shields)
                RefreshItemCategory(icon, shieldsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in bows)
                RefreshItemCategory(icon, bowsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Equipment>());

            foreach (Sprite icon in scrolls)
                RefreshItemCategory(icon, scrollsPath + FilterName(icon.name), ScriptableObject.CreateInstance<Scroll>());

            foreach (Sprite icon in supplies)
                RefreshItemCategory(icon, suppliesPath + FilterName(icon.name), ScriptableObject.CreateInstance<Supply>());

            foreach (Sprite icon in gems)
                RefreshItemCategory(icon, gemPath + FilterName(icon.name), ScriptableObject.CreateInstance<Gem>());

            foreach (Sprite icon in questItems)
                RefreshItemCategory(icon, questItemPath + FilterName(icon.name), ScriptableObject.CreateInstance<QuestItem>());
        }
        
        void RefreshItemCategory(Sprite icon, string path, IItem scriptableObject)
        {
            prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabsRootPath + path + ".prefab", typeof(GameObject));
            asset = (IItem)AssetDatabase.LoadAssetAtPath(assetsRootPath + path + ".asset", typeof(IItem));

            if (prefab == null)
                prefab = CreateOrReplacePrefab(icon, path);
            else if (refreshPrefabs)
                RefreshPrefab(icon, prefab, path);

            if (asset == null)
                CreateScriptableObject(icon, prefab, scriptableObject, path);
            else
                RefreshScriptableObject(icon, prefab, path);
        }

        GameObject CreateOrReplacePrefab(Sprite icon, string path)
        {
            obj = new GameObject(FilterName(icon.name));
            obj.AddComponent<BoxCollider2D>().isTrigger = true;
            obj.AddComponent<Item>();
            renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = icon;
            renderer.sharedMaterial = material;

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
                && CompareNames(icon.name, prefab.name))
                return;

            CreateOrReplacePrefab(icon, path);
        }

        void CreateScriptableObject(Sprite icon, GameObject prefab, IItem scriptableObject, string path)
        {
            AssetDatabase.CreateAsset((ItemBase)scriptableObject, assetsRootPath + path + ".asset");
            RefreshScriptableObject(icon, prefab, path);
        }

        void RefreshScriptableObject(Sprite icon, GameObject prefab, string path)
        {
            asset = (IItem)AssetDatabase.LoadAssetAtPath(assetsRootPath + path + ".asset", typeof(IItem));

            if (asset is Equipment 
                && CompareItemSprite(((Equipment)asset).itemSprite, itemSprites[icon.name]) 
                && asset.icon == icon 
                && asset.prefab == prefab 
                && CompareNames(icon.name, asset.itemName))
                return;
            else if (asset is not Equipment 
                && asset.icon == icon 
                && asset.prefab == prefab 
                && CompareNames(icon.name, asset.itemName))
                return;

            ((ItemBase)asset).itemName = FilterName(icon.name);

            ((ItemBase)asset).icon = icon;

            if (asset is Equipment)
                ((Equipment)asset).itemSprite = itemSprites[icon.name];
           
            ((ItemBase)asset).prefab = prefab;

            EditorUtility.SetDirty((ScriptableObject)asset);
            AssetDatabase.SaveAssetIfDirty((ScriptableObject)asset);
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