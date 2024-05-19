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
        [SerializeField] RectTransform cutsceneDisplay;
        [SerializeField] RectTransform equipmentDisplay;
        [SerializeField] RectTransform scrollDisplay;
        [SerializeField] RectTransform commandDisplay;
        [SerializeField] RectTransform optionsDisplay;
        [SerializeField] RectTransform gameOverDisplay;

        [SerializeField] Image promptImage;
        [SerializeField] TMP_Text promptText;

        [SerializeField] Camera mainCamera;
        [SerializeField] Transform allieRoot;
        [SerializeField] Transform enemyRoot;
        [SerializeField] Transform popupRoot;

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

        public Camera getCamera => mainCamera;
        public Actors allies { get; private set; } = new Actors();
        public Actors enemies { get; private set; } = new Actors();

        public Dictionary<IItem.Category, Inventory> inventories { get; private set; } = new Dictionary<IItem.Category, Inventory>();
        
        public Queue<IActor> aTBGuageFilledQueue { get; set; } = new Queue<IActor>();
        public LinkedList<Command> pendingCommands { get; set; } = new LinkedList<Command>();
        public LinkedList<Command> successfulCommands { get; set; } = new LinkedList<Command>();
        public Queue<ActionBase> cutsceneActions { get; set; } = new Queue<ActionBase>();

        public Image getPromptImage => promptImage;
        public TMP_Text getPromptText => promptText;


        ShakeData IGlobal.getShakeData => shakeData;
        AudioSource IGlobal.getAudioSource => audioSource; 

        IInventory IGlobal.getCompletedQuests => completedQuests;
        IInventory IGlobal.getCompletedIds => completedIds;

        IGlobal.GameState gameState = IGlobal.GameState.Stopped;

        public RectTransform getCanvas => canvas;

        public int sceneIDToLoad { get; set; } = 0;
        public Vector2 scenePositionToStart { get; set; } = Vector2.zero;
        public float sceneEulerAngleZToStart { get; set; } = 0;

        public void Awake()
        {
            factory = GetComponent<IFactory>();

            allies = new Actors(allieRoot);

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
            displays.Add(IGlobal.Display.CutsceneDisplay, cutsceneDisplay);
            displays.Add(IGlobal.Display.EquipmentDisplay, equipmentDisplay);
            displays.Add(IGlobal.Display.ScrollDisplay, scrollDisplay);
            displays.Add(IGlobal.Display.CommandDisplay, commandDisplay);
            displays.Add(IGlobal.Display.OptionsDisplay, optionsDisplay);
            displays.Add(IGlobal.Display.GameOverDisplay, gameOverDisplay);

            IActor river = GameObject.Find("/DontDestroyOnLoad/AllieRoot/River").GetComponent<IActor>();
            allies.Add(river);
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

        public void ClearAllInventories()
        {
            foreach (KeyValuePair<IItem.Category, Inventory> inventory in inventories)
                inventory.Value.Clear();
        }

        public void AddDamagePopup(string damage, Vector3 position)
        {
            GameObject obj = Instantiate(factory.getDamagePopupPrefab, position, Quaternion.identity, popupRoot);
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = damage;
            Destroy(obj, 3f);
        }

        public void AddRecoveryPopup(string recovery, Vector3 position)
        {
            GameObject obj = Instantiate(factory.getRecoveryPopupPrefab, position, Quaternion.identity, popupRoot);
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = recovery;
            Destroy(obj, 3f);
        }

        public void ClearAllPopups()
        {
            for (int n = popupRoot.transform.childCount - 1; n > -1; n--)
                Destroy(popupRoot.transform.GetChild(n).gameObject);
        }
    }
}
