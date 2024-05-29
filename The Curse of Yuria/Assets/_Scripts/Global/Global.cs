using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;
using TMPro;
using System.Linq;

public class Global : MonoBehaviour
{
    public enum Display { MainMenu, Loading, Cutscene, Equipment, Scroll, Command, Options, GameOver, ObtainedItems }
    public enum GameState { Playing, Paused, Stopped }

    public static Global instance { get; set; }

    [SerializeField] ShakeData shakeData;
    [SerializeField] AudioSource audioSource;

    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform display2;
    [SerializeField] RectTransform mainMenuDisplay;
    [SerializeField] RectTransform loadingDisplay;
    [SerializeField] RectTransform cutsceneDisplay;
    [SerializeField] RectTransform equipmentDisplay;
    [SerializeField] RectTransform scrollDisplay;
    [SerializeField] RectTransform commandDisplay;
    [SerializeField] RectTransform optionsDisplay;
    [SerializeField] RectTransform gameOverDisplay;
    [SerializeField] RectTransform obtainedItemsDisplay;

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

    Dictionary<Display, RectTransform> displays = new Dictionary<Display, RectTransform>();

    public Camera getCamera => mainCamera;
    public Actors allies { get; private set; } = new Actors();
    public Actors enemies { get; private set; } = new Actors();

    public Dictionary<string, Inventory> inventories { get; private set; } = new Dictionary<string, Inventory>();

    public Queue<IActor> aTBGuageFilledQueue { get; set; } = new Queue<IActor>();
    public LinkedList<Command> pendingCommands { get; set; } = new LinkedList<Command>();
    public LinkedList<Command> successfulCommands { get; set; } = new LinkedList<Command>();
    public Queue<ActionBase> cutsceneActions { get; set; } = new Queue<ActionBase>();
    public List<string> obtainedItems { get; set; } = new List<string>();

    public Image getPromptImage => promptImage;
    public TMP_Text getPromptText => promptText;

    public ShakeData getShakeData => shakeData;
    public AudioSource getAudioSource => audioSource;

    public Inventory getCompletedQuests => completedQuests;
    public Inventory getCompletedIds => completedIds;

    public GameState gameState { get; set; } = GameState.Stopped;
    GameState previousGameState = GameState.Playing;

    public RectTransform getCanvas => canvas;
    public RectTransform getDisplay2 => display2;

    public int sceneIDToLoad { get; set; } = 0;
    public Vector2 scenePositionToStart { get; set; } = Vector2.zero;
    public float sceneEulerAngleZToStart { get; set; } = 0;

    public void Awake()
    {
        instance = this;

        allies = new Actors(allieRoot);

        inventories.Add(Factory.instance.getHelmet.name, helmets);
        inventories.Add(Factory.instance.getEarring.name, earrings);
        inventories.Add(Factory.instance.getGlasses.name, glasses);
        inventories.Add(Factory.instance.getMask.name, masks);
        inventories.Add(Factory.instance.getMelee1H.name, meleeWeapons1H);
        inventories.Add(Factory.instance.getMelee2H.name, meleeWeapons2H);
        inventories.Add(Factory.instance.getCape.name, capes);
        inventories.Add(Factory.instance.getArmor.name, armor);
        inventories.Add(Factory.instance.getShield.name, shields);
        inventories.Add(Factory.instance.getBow.name, bows);
        inventories.Add(Factory.instance.getBasic.name, supplies);
        inventories.Add(Factory.instance.getScroll.name, scrolls);
        inventories.Add(Factory.instance.getQuestItem.name, questItems);

        displays.Add(Display.MainMenu, mainMenuDisplay);
        displays.Add(Display.Loading, loadingDisplay);
        displays.Add(Display.Cutscene, cutsceneDisplay);
        displays.Add(Display.Equipment, equipmentDisplay);
        displays.Add(Display.Scroll, scrollDisplay);
        displays.Add(Display.Command, commandDisplay);
        displays.Add(Display.Options, optionsDisplay);
        displays.Add(Display.GameOver, gameOverDisplay);
        displays.Add(Display.ObtainedItems, obtainedItemsDisplay);

        IActor river = GameObject.Find("/DontDestroyOnLoad/AllieRoot/River").GetComponent<IActor>();
        river.Initialize();
        allies.Add(river);
    }

    public void LateUpdate()
    {
        if (gameState == previousGameState)
            return;

        switch (gameState)
        {
            case GameState.Playing:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1f;
                break;
            case GameState.Stopped:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                break;
        }

        previousGameState = gameState;
    }

    public void ToggleDisplay(Display display)
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
        foreach (KeyValuePair<string, Inventory> inventory in inventories)
            inventory.Value.Clear();
    }

    public void AddDamagePopup(string damage, Vector3 position)
    {
        GameObject obj = Instantiate(Factory.instance.getDamagePopupPrefab, position, Quaternion.identity, popupRoot);
        obj.transform.GetChild(0).GetComponent<TMP_Text>().text = damage;
        Destroy(obj, 3f);
    }

    public void AddRecoveryPopup(string recovery, Vector3 position)
    {
        GameObject obj = Instantiate(Factory.instance.getRecoveryPopupPrefab, position, Quaternion.identity, popupRoot);
        obj.transform.GetChild(0).GetComponent<TMP_Text>().text = recovery;
        Destroy(obj, 3f);
    }

    public void ClearAllPopups()
    {
        for (int n = popupRoot.transform.childCount - 1; n > -1; n--)
            Destroy(popupRoot.transform.GetChild(n).gameObject);
    }
}

