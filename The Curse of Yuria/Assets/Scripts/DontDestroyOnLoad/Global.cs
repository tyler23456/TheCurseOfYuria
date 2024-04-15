using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Global : MonoBehaviour, IGlobal
    {
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

        public void Awake()
        {

        }

        public void AddToInventory(HeroEditor.Common.Data.ItemSprite item, HeroEditor.Common.Enums.EquipmentPart part, Color? color)
        {
            switch (part)
            {
                case HeroEditor.Common.Enums.EquipmentPart.Helmet:
                    helmets.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.Earrings:
                    earrings.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.Glasses:
                    glasses.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.MeleeWeapon1H:
                    meleeWeapons1H.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.MeleeWeapon2H:
                    meleeWeapons2H.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.Cape:
                    capes.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.Armor:
                    helmets.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.Shield:
                    shields.Add(item.Name, 1);
                    break;
                case HeroEditor.Common.Enums.EquipmentPart.Bow:
                    bows.Add(item.Name, 1);
                    break;
            }
        }
    }
}