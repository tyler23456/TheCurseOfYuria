using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HeroEditor.Common.Enums;

namespace TCOY.DontDestroyOnLoad
{
    [ExecuteInEditMode]
    public class ScriptableObjectManager : MonoBehaviour
    {
        [SerializeField] string iconsRootPath = "Assets/User/Plugins/HeroEditor/FantasyHeroes/Icons/Equipment/";
        [SerializeField] string spriteRootPath = "Assets/User/Plugins/HeroEditor/FantasyHeroes/Sprites/Equipment/";

        [SerializeField] string helmetsPath = "Helmet";
        [SerializeField] string earringsPath = "Earrings";
        [SerializeField] string glassesPath = "Glasses";
        [SerializeField] string masksPath = "Mask";
        [SerializeField] string meleeWeapon1HPath = "MeleeWeapon1H";
        [SerializeField] string meleeWeapon2HPath = "MeleeWeapon2H";
        [SerializeField] string capesPath = "Cape";
        [SerializeField] string armorPath = "Armor";
        [SerializeField] string shieldsPath = "Shield";
        [SerializeField] string bowsPath = "Bow";
        [SerializeField] string suppliesPath = "Supplies";
        [SerializeField] string questItemPath = "KeyItem";

        [SerializeField] string assetsRootPath = "Assets/_Scriptable/";
        [SerializeField] string prefabsRootPath = "Assets/User/Prefabs/Items/";

        [SerializeField] bool refresh;

        void Update()
        {
            if (!refresh)
                return;

            refresh = false;

            //need to check for duplicates and plan accordingly

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
            Sprite[] supplies = Resources.LoadAll<Sprite>(iconsRootPath + suppliesPath);
            Sprite[] questItems = Resources.LoadAll<Sprite>(iconsRootPath + questItemPath);

            Sprite[] sprites = Resources.LoadAll<Sprite>(spriteRootPath);
            Dictionary<string, Sprite> spriteLookup = new Dictionary<string, Sprite>();

            foreach (Sprite sprite in sprites)
                spriteLookup.Add(sprite.name, sprite);


            foreach (Sprite icon in helmets)
                RefreshEquipment(icon, helmetsPath);

            foreach (Sprite icon in earrings)
                RefreshEquipment(icon, earringsPath);

            foreach (Sprite icon in glasses)
                RefreshEquipment(icon, glassesPath);

            foreach (Sprite icon in masks)
                RefreshEquipment(icon, masksPath);

            foreach (Sprite icon in meleeWeapon1H)
                RefreshEquipment(icon, meleeWeapon1HPath);

            foreach (Sprite icon in meleeWeapon2H)
                RefreshEquipment(icon, meleeWeapon2HPath);

            foreach (Sprite icon in capes)
                RefreshEquipment(icon, capesPath);

            foreach (Sprite icon in armor)
                RefreshEquipment(icon, armorPath);

            foreach (Sprite icon in shields)
                RefreshEquipment(icon, shieldsPath);

            foreach (Sprite icon in bows)
                RefreshEquipment(icon, bowsPath);

            foreach (Sprite icon in supplies)
                RefreshSupplies(icon, suppliesPath);

            foreach (Sprite icon in questItems)
                RefreshQuestItems(icon, questItemPath);
        }

        void RefreshEquipment(Sprite icon, string path)
        {
            
            CreateScriptableObject(icon, CreatePrefab(icon, path), new Equipable() , path);
        }

        void RefreshSupplies(Sprite icon, string path)
        {
            CreateScriptableObject(icon, CreatePrefab(icon, path), new Equipable(), path);
        }

        void RefreshQuestItems(Sprite icon, string path)
        {
            CreatePrefab(icon, path);
        }

        GameObject CreatePrefab(Sprite icon, string path)
        {
            GameObject obj = new GameObject(icon.name);
            obj.AddComponent<BoxCollider2D>().isTrigger = true;
            obj.AddComponent<Interactables.Item>(); //this cannot be referenced.  Not sure how to go about this.  May need to make this part of interactables system;----XXXXXXXX-----------XXXXXXXX
            PrefabUtility.SaveAsPrefabAssetAndConnect(obj, path, InteractionMode.UserAction);
            return obj;
        }

        void CreateScriptableObject(Sprite icon, GameObject prefab, ScriptableObject scriptableObject, string path)
        {
            AssetDatabase.CreateAsset(scriptableObject, path);
        }
    }
}