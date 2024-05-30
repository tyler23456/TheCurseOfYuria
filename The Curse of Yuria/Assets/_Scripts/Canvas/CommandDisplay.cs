using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;


public class CommandDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] RectTransform display;

    [SerializeField] Button buttonPrefab;
    [SerializeField] RectTransform grid;
    [SerializeField] Button attackTab;
    [SerializeField] Button magicTab;
    [SerializeField] Button itemTab;

    IActor currentPartyMember;
    string commandName = "None";
    IActor target = null;

    InventoryUI skillInventoryUI;
    InventoryUI itemInventoryUI;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        display.gameObject.SetActive(true);

        skillInventoryUI = new InventoryUI();
        itemInventoryUI = new InventoryUI();

        currentPartyMember = Global.Instance.aTBGuageFilledQueue.Peek();

        attackTab.onClick.RemoveAllListeners();
        magicTab.onClick.RemoveAllListeners();
        itemTab.onClick.RemoveAllListeners();

        attackTab.onClick.AddListener(() => OnClickAttack());
        magicTab.onClick.AddListener(() => OnClickSkill());
        itemTab.onClick.AddListener(() => OnClickItems());

        commandName = "None";
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MarkerManager.instance.DestroyAllMarkers();
        Global.Instance.gameState = Global.GameState.Playing;
    }

    public void OnClickAttack()
    {
        string weapon = currentPartyMember.getEquipment.Find(i =>
        Factory.instance.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon1H ||
        Factory.instance.GetItem(i).itemType.part == EquipmentPart.MeleeWeapon2H ||
        Factory.instance.GetItem(i).itemType.part == EquipmentPart.Bow);

        if (weapon == null)
            return;

        OnSelectAttack(weapon);
    }

    public void OnClickSkill()
    {
        skillInventoryUI.displayName = true;
        skillInventoryUI.displayCount = false;
        skillInventoryUI.grid = grid;
        skillInventoryUI.buttonPrefab = buttonPrefab;
        skillInventoryUI.OnClick = (commandName) => OnSelectSkill(commandName);
        skillInventoryUI.inventory = currentPartyMember.getSkills;
        skillInventoryUI.onPointerEnter = (itemName) => { };
        skillInventoryUI.onPointerExit = (itemName) => { };
        skillInventoryUI.Display();
    }

    public void OnClickItems()
    {
        itemInventoryUI.grid = grid;
        itemInventoryUI.buttonPrefab = buttonPrefab;
        itemInventoryUI.OnClick = (commandName) => OnSelectItem(commandName);
        itemInventoryUI.inventory = Global.Instance.inventories[Factory.instance.getBasic.name];
        itemInventoryUI.onPointerEnter = (itemName) => { };
        itemInventoryUI.onPointerExit = (itemName) => { };
        itemInventoryUI.Display();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            commandName = "None";
            display.gameObject.SetActive(true);
            MarkerManager.instance.DestroyAllMarkers();
        }

        target = null;

        if (commandName == "None")
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

        foreach (RaycastHit2D hit in hits)
        {
            target = hit.transform.GetComponent<IActor>();

            if (target != null)
                break;
        }

        if (target == null)
        {
            MarkerManager.instance.DestroyAllMarkers();
            return;
        }

        if (MarkerManager.instance.count == 0)
            MarkerManager.instance.AddMarker();

        MarkerManager.instance.SetMarkerMessageAt(0, "Use " + commandName + " on " + target.getGameObject.name);
        MarkerManager.instance.SetMarkerWorldPositionAt(0, target.getCollider2D.bounds.center + Vector3.up * target.getCollider2D.bounds.extents.y);

        if (Input.GetMouseButtonDown(0))
        {
            OnSelectTarget(target);
        }
    }

    void OnSelectCommand(string commandName)
    {
        this.commandName = commandName;
        display.gameObject.SetActive(false);
    }

    void OnSelectAttack(string commandName)
    {
        OnSelectCommand(commandName);
    }

    void OnSelectSkill(string commandName)
    {
        if (currentPartyMember.getStats.MP >= Factory.instance.GetItem(commandName).getCost)
            OnSelectCommand(commandName);
        else
            NotificationManager.Instance.Notify("you do not have enough MP");
    }

    void OnSelectItem(string commandName)
    {
        Global.Instance.inventories[Factory.instance.GetItem(commandName).itemType.name].Remove(commandName);
        OnSelectCommand(commandName);
    }

    public void OnSelectTarget(IActor target)
    {
        Command command = new Command(currentPartyMember, Factory.instance.GetItem(commandName), new List<IActor> { target });
        Global.Instance.pendingCommands.AddLast(command);
        currentPartyMember.getATBGuage.Reset();
        Global.Instance.aTBGuageFilledQueue.Dequeue();
        gameObject.SetActive(false);

    }
}
