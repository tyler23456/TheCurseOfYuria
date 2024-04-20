using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;

namespace TCOY.DontDestroyOnLoad
{
    public class Global : MonoBehaviour, IGlobal
    {
        IFactory factory;

        [SerializeField] Camera mainCamera;
        [SerializeField] List<IPlayer> party;

        Inventory helmets { get; }
        Inventory earrings { get; }
        Inventory glasses { get; }
        Inventory masks { get; }
        Inventory meleeWeapons1H { get; }
        Inventory meleeWeapons2H { get; }
        Inventory capes { get; }
        Inventory armor { get; }
        Inventory shields { get; }
        Inventory bows { get; }
        Inventory supplies { get; }
        Inventory questItems { get; }
        Inventory completedQuests { get; }
        Inventory completedIds { get; }

        public Camera getCamera => mainCamera;
        List<IPlayer> IGlobal.getParty => party;
        IInventory IGlobal.getHelmets => helmets;
        IInventory IGlobal.getEarrings => earrings;
        IInventory IGlobal.getGlasses => glasses;
        IInventory IGlobal.getMasks => masks;
        IInventory IGlobal.getMeleeWeapons1H => meleeWeapons1H;
        IInventory IGlobal.getMeleeWeapons2H => meleeWeapons2H;
        IInventory IGlobal.getCapes => capes;
        IInventory IGlobal.getArmor => armor;
        IInventory IGlobal.getShields => shields;
        IInventory IGlobal.getBows => bows;
        IInventory IGlobal.getSupplies => supplies;
        IInventory IGlobal.getQuestItems => questItems;
        IInventory IGlobal.getCompletedQuests => completedQuests;
        IInventory IGlobal.getCompletedIds => completedIds;

        public void Awake()
        {
            factory = GetComponent<IFactory>();
        }

        public IInventory GetInventoryOf(string itemName)
        {
            ItemSprite item = factory.itemSprites[itemName];
            string partString = item.Id.Split('.')[2];

            switch (partString)
            {
                case "Helmet":
                    return helmets;
                case "Earrings":
                    return earrings;
                case "Glasses":
                    return glasses;
                case "MeleeWeapon1H":
                    return meleeWeapons1H;
                case "MeleeWeapon2H":
                    return meleeWeapons2H;
                case "Cape":
                    return capes;
                case "Armor":
                    return armor;
                case "Shield":
                    return shields;
                case "Bow":
                    return bows;
            }
            return null;
        }
    }
}