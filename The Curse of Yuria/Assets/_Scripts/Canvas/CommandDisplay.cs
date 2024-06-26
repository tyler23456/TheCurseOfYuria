using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;


public class CommandDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] StatusEffectBase KOStatusEffect;

    [SerializeField] RectTransform display;

    [SerializeField] Button buttonPrefab;
    [SerializeField] RectTransform grid;
    [SerializeField] Button attackTab;
    [SerializeField] Button magicTab;
    [SerializeField] Button itemTab;

    IActor currentAllie;
    string commandName = "None";
    List<IActor> potentialTargets = new List<IActor>();
    IActor target = null;

    InventoryUI attackInventoryUI;
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

        attackInventoryUI = new InventoryUI();
        skillInventoryUI = new InventoryUI();
        itemInventoryUI = new InventoryUI();

        currentAllie = BattleManager.Instance.PeekNextATBGuageFilled();

        attackTab.onClick.RemoveAllListeners();
        magicTab.onClick.RemoveAllListeners();
        itemTab.onClick.RemoveAllListeners();

        attackTab.onClick.AddListener(() => OnClickAttack());
        magicTab.onClick.AddListener(() => OnClickSkill());
        itemTab.onClick.AddListener(() => OnClickItems());

        commandName = "None";

        int newAllieIndex = currentAllie.obj.transform.GetSiblingIndex();
        AllieManager.Instance.SwapIndexes(0, newAllieIndex);

        RefreshGridWithAttackOptions();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MarkerManager.instance.DestroyAllMarkers();
        GameStateManager.Instance.Play();
    }

    public void RefreshGridWithAttackOptions()
    {
        attackInventoryUI.grid = grid;
        attackInventoryUI.buttonPrefab = buttonPrefab;
        attackInventoryUI.OnClick = (commandName) => OnSelectItem(commandName);
        attackInventoryUI.inventory = new Inventory();
        attackInventoryUI.onPointerEnter = (itemName) => { };
        attackInventoryUI.onPointerExit = (itemName) => { };
        attackInventoryUI.Display();
    }

    public void OnClickAttack()
    {
        string weapon = currentAllie.getEquipment.Find(i =>
        ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon1H ||
        ItemDatabase.Instance.GetPart(i) == EquipmentPart.MeleeWeapon2H ||
        ItemDatabase.Instance.GetPart(i) == EquipmentPart.Bow);

        RefreshGridWithAttackOptions();

        if (weapon == null)
            return;

        OnSelectAttack(weapon);
    }

    public void OnClickSkill()
    {
        skillInventoryUI.showName = true;
        skillInventoryUI.showCount = false;
        skillInventoryUI.grid = grid;
        skillInventoryUI.buttonPrefab = buttonPrefab;
        skillInventoryUI.OnClick = (commandName) => OnSelectSkill(commandName);
        skillInventoryUI.inventory = currentAllie.getScrolls;
        skillInventoryUI.onPointerEnter = (itemName) => { };
        skillInventoryUI.onPointerExit = (itemName) => { };
        skillInventoryUI.Display();
    }

    public void OnClickItems()
    {
        itemInventoryUI.grid = grid;
        itemInventoryUI.buttonPrefab = buttonPrefab;
        itemInventoryUI.OnClick = (commandName) => OnSelectItem(commandName);
        itemInventoryUI.inventory = InventoryManager.Instance.basic;
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
            MarkerManager.instance.DestroyAllMarkersWith("CommandDisplayMarker");
        }

        target = null;

        if (commandName == "None")
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);
        potentialTargets.Clear();

        foreach (RaycastHit2D hit in hits)
        {
            target = hit.transform.GetComponent<IActor>();

            if (target != null)
                potentialTargets.Add(target);
        }

        ISkill item = (ISkill)ItemDatabase.Instance.Get(commandName);
        bool containsKO = item.TrueForAnyStatusEffect(i => i is IRestoration && ((IRestoration)i).ContainsStatusEffectToRemove(KOStatusEffect.name));

        target = null;
        if (containsKO)
        {
            foreach (IActor potentialTarget in potentialTargets)
            {
                if (potentialTarget.getStatusEffects.Contains(KOStatusEffect.name))
                {
                    target = potentialTarget;
                    break;
                }  
            }
        } 
        else
        {
            foreach (IActor potentialTarget in potentialTargets)
            {
                if (!potentialTarget.getStatusEffects.Contains(KOStatusEffect.name))
                {
                    target = potentialTarget;
                    break;
                }
            }
        }

        if (target == null)
        {
            MarkerManager.instance.DestroyAllMarkersWith("CommandDisplayMarker");
            return;
        }

        if (MarkerManager.instance.Count("CommandDisplayMarker") == 0)
            MarkerManager.instance.AddMarker("CommandDisplayMarker");

        MarkerManager.instance.SetMarkerMessageAt("CommandDisplayMarker", "Use " + commandName + " on " + target.obj.name);
        MarkerManager.instance.SetMarkerWorldPositionAt("CommandDisplayMarker", target.getCollider2D.bounds.center + Vector3.up * target.getCollider2D.bounds.extents.y);

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
        if (currentAllie.getStats.MP >= ((IScroll)ItemDatabase.Instance.Get(commandName)).getCost)
            OnSelectCommand(commandName);
        else
            NotificationManager.Instance.Notify("you do not have enough MP");
    }

    void OnSelectItem(string commandName)
    {
        InventoryManager.Instance.basic.Remove(commandName);
        OnSelectCommand(commandName);
    }

    public void OnSelectTarget(IActor target)
    {
        Command command = new Command(currentAllie, ItemDatabase.Instance.Get(commandName), new List<IActor> { target });
        BattleManager.Instance.AddCommand(command);
        currentAllie.getATBGuage.Reset();
        BattleManager.Instance.RemoveNextATBGuageFilled();
        gameObject.SetActive(false);

    }
}
