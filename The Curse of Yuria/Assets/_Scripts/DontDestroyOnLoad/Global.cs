using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using UnityEngine.UI;

namespace TCOY.DontDestroyOnLoad
{
    public class Global : MonoBehaviour, IGlobal
    {
        IFactory factory;

        [SerializeField] RectTransform titleScreenDisplay;
        [SerializeField] RectTransform promptDisplay;
        [SerializeField] RectTransform equipmentDisplay;
        [SerializeField] RectTransform commandDisplay;
        [SerializeField] RectTransform optionsDisplay;
        [SerializeField] RectTransform gameOverDisplay;
        
        [SerializeField] Camera mainCamera;
        [SerializeField] List<IPlayer> party;
        [SerializeField] List<IActor> actors;

        Inventory helmets;
        Inventory earrings;
        Inventory glasses;
        Inventory masks;
        Inventory meleeWeapons1H;
        Inventory meleeWeapons2H;
        Inventory capes;
        Inventory armor;
        Inventory shields;
        Inventory bows;
        Inventory scrolls;
        Inventory supplies;
        Inventory questItems;
        Inventory completedQuests;
        Inventory completedIds;

        public List<IPlayer> getParty => party;
        public List<IActor> getActors => actors;
        public Camera getCamera => mainCamera;

        public Dictionary<IItem.Category, Inventory> inventories { get; private set; } = new Dictionary<IItem.Category, Inventory>();

        public Queue<IPlayer.Names> aTBGuageFilledQueue { get; set; } = new Queue<IPlayer.Names>();
        public Queue<ICommand> commandQueue { get; set; } = new Queue<ICommand>();

        IInventory IGlobal.getCompletedQuests => completedQuests;
        IInventory IGlobal.getCompletedIds => completedIds;

        public void Awake()
        { 
            factory = GetComponent<IFactory>();

            inventories.Add(IItem.Category.helmets, helmets);
            inventories.Add(IItem.Category.earrings, earrings);
            inventories.Add(IItem.Category.glasses, glasses);
            inventories.Add(IItem.Category.masks, masks);
            inventories.Add(IItem.Category.meleeWeapons1H, meleeWeapons1H);
            inventories.Add(IItem.Category.meleeWeapons2H, meleeWeapons2H);
            inventories.Add(IItem.Category.capes, capes);
            inventories.Add(IItem.Category.armor, armor);
            inventories.Add(IItem.Category.shields, shields);
            inventories.Add(IItem.Category.bows, bows);
            inventories.Add(IItem.Category.supplies, supplies);
            inventories.Add(IItem.Category.scrolls, scrolls);
            inventories.Add(IItem.Category.questItems, questItems);

        }
    }
}
