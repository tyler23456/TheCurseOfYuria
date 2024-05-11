using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;

namespace TCOY.DontDestroyOnLoad
{
    public class Global : MonoBehaviour, IGlobal
    {
        IFactory factory;

        [SerializeField] ShakeData shakeData;
        [SerializeField] AudioSource audioSource;

        [SerializeField] RectTransform canvas;
        [SerializeField] RectTransform titleScreenDisplay;
        [SerializeField] RectTransform promptDisplay;
        [SerializeField] RectTransform equipmentDisplay;
        [SerializeField] RectTransform scrollDisplay;
        [SerializeField] RectTransform commandDisplay;
        [SerializeField] RectTransform optionsDisplay;
        [SerializeField] RectTransform gameOverDisplay;
        
        [SerializeField] Camera mainCamera;
        [SerializeField] GameObject partyRoot;

        List<IActor> actors = new List<IActor>();

        Inventory helmets = new Inventory();
        Inventory earrings = new Inventory();
        Inventory glasses = new Inventory();
        Inventory masks = new Inventory();
        Inventory meleeWeapons1H = new Inventory();
        Inventory meleeWeapons2H = new Inventory();
        Inventory capes = new Inventory();
        Inventory armor = new Inventory();
        Inventory shields = new Inventory();
        Inventory bows = new Inventory();
        Inventory scrolls = new Inventory();
        Inventory supplies = new Inventory();
        Inventory questItems = new Inventory();
        Inventory completedQuests = new Inventory();
        Inventory completedIds = new Inventory();

        public GameObject getPartyRoot => partyRoot;
        public List<IActor> getActors => actors;
        public Camera getCamera => mainCamera;

        public Dictionary<IItem.Category, Inventory> inventories { get; private set; } = new Dictionary<IItem.Category, Inventory>();

        public Queue<IActor> aTBGuageFilledQueue { get; set; } = new Queue<IActor>();
        public Queue<ICommand> pendingCommands { get; set; } = new Queue<ICommand>();
        public Stack<ICommand> successfulCommands { get; set; } = new Stack<ICommand>();


        ShakeData IGlobal.getShakeData => shakeData;
        AudioSource IGlobal.getAudioSource => audioSource; 

        IInventory IGlobal.getCompletedQuests => completedQuests;
        IInventory IGlobal.getCompletedIds => completedIds;

        public RectTransform getCanvas => canvas;
        public RectTransform getTitleScreenDisplay => titleScreenDisplay;
        public RectTransform getPromptDisplay => promptDisplay;
        public RectTransform getEquipmentDisplay => equipmentDisplay;
        public RectTransform getScrollDisplay => scrollDisplay;
        public RectTransform getCommandDisplay => commandDisplay;
        public RectTransform getOptionsDisplay => optionsDisplay;
        public RectTransform getGameOverDisplay => gameOverDisplay;

        public int getPartyMemberCount => partyRoot.transform.childCount;

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

        public IPartyMember GetPartyMember(int i)
        {
            return partyRoot.transform.GetChild(i).GetComponent<IPartyMember>();
        }
    }
}
