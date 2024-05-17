using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;
using TMPro;
using System.Linq;

namespace TCOY.DontDestroyOnLoad
{
    public class Global : MonoBehaviour, IGlobal
    {
        IFactory factory;

        [SerializeField] ShakeData shakeData;
        [SerializeField] AudioSource audioSource;

        [SerializeField] RectTransform canvas;
        [SerializeField] RectTransform mainMenuDisplay;
        [SerializeField] RectTransform loadingDisplay;
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

        Dictionary<IGlobal.Display, RectTransform> displays = new Dictionary<IGlobal.Display, RectTransform>();

        public GameObject getPartyRoot => partyRoot;
        public List<IActor> getActors => actors;
        public Camera getCamera => mainCamera;

        public Dictionary<IItem.Category, Inventory> inventories { get; private set; } = new Dictionary<IItem.Category, Inventory>();
        

        public Queue<IActor> aTBGuageFilledQueue { get; set; } = new Queue<IActor>();
        public Queue<ICommand> pendingCommands { get; set; } = new Queue<ICommand>();
        public Stack<ICommand> successfulCommands { get; set; } = new Stack<ICommand>();

        public Queue<string> promptQueue { get; set; } = new Queue<string>();


        ShakeData IGlobal.getShakeData => shakeData;
        AudioSource IGlobal.getAudioSource => audioSource; 

        IInventory IGlobal.getCompletedQuests => completedQuests;
        IInventory IGlobal.getCompletedIds => completedIds;

        IGlobal.GameState gameState = IGlobal.GameState.Stopped;

        public RectTransform getCanvas => canvas;

        public int getPartyMemberCount => partyRoot.transform.childCount;

        public int sceneIDToLoad { get; set; } = 0;
        public Vector2 scenePositionToStart { get; set; } = Vector2.zero;
        public float sceneEulerAngleZToStart { get; set; } = 0;

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

            displays.Add(IGlobal.Display.MainMenuDisplay, mainMenuDisplay);
            displays.Add(IGlobal.Display.LoadingDisplay, loadingDisplay);
            displays.Add(IGlobal.Display.PromptDisplay, promptDisplay);
            displays.Add(IGlobal.Display.EquipmentDisplay, equipmentDisplay);
            displays.Add(IGlobal.Display.ScrollDisplay, scrollDisplay);
            displays.Add(IGlobal.Display.CommandDisplay, commandDisplay);
            displays.Add(IGlobal.Display.OptionsDisplay, optionsDisplay);
            displays.Add(IGlobal.Display.GameOverDisplay, gameOverDisplay);
        }

        public void LateUpdate()
        {
            if (IGlobal.gameState == gameState)
                return;

            gameState = IGlobal.gameState;

            switch (gameState)
            {
                case IGlobal.GameState.Playing:
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1f;
                    break;
                case IGlobal.GameState.Paused:
                case IGlobal.GameState.Stopped:
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0f;
                    break;
            }
        }

        public void ToggleDisplay(IGlobal.Display display)
        {
            if (displays[display].gameObject.activeSelf == true)
            {
                displays[display].gameObject.SetActive(false);
            }             
            else
            {
                foreach (RectTransform t in canvas)
                    t.gameObject.SetActive(false);

                displays[display].gameObject.SetActive(true);
            }
        }

        public IPartyMember GetPartyMember(int i)
        {
            return partyRoot.transform.GetChild(i).GetComponent<IPartyMember>();
        }

        public void ClearAllInventories()
        {
            foreach (KeyValuePair<IItem.Category, Inventory> inventory in inventories)
                inventory.Value.Clear();
        }

        public void DestroyAllPartyMembers()
        {
            for (int n = getPartyRoot.transform.childCount - 1; n > -1; n--)
                Destroy(getPartyRoot.transform.GetChild(n).gameObject);
        }

    }
}
