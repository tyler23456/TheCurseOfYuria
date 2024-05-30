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
    public enum GameState { Playing, Paused, Stopped }

    public static Global Instance { get; set; }

    [SerializeField] ShakeData shakeData;
    [SerializeField] AudioSource audioSource;

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

    public void Awake()
    {
        Instance = this;

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

    public void ClearAllInventories()
    {
        foreach (KeyValuePair<string, Inventory> inventory in inventories)
            inventory.Value.Clear();
    }
}

