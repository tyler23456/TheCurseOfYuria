using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;


public class CommandDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] Skill defaultAttack;
    [SerializeField] StatusEffectBase KOStatusEffect;

    [SerializeField] RectTransform display;

    [SerializeField] Button buttonPrefab;
    [SerializeField] RectTransform grid;
    [SerializeField] Button attackTab;
    [SerializeField] Button magicTab;
    [SerializeField] Button itemTab;
    [SerializeField] Button exitButton;

    IActor currentAllie;
    string commandName = "None";
    List<IActor> potentialTargets = new List<IActor>();
    IActor target = null;

    List<IActor> aTBGuagesFilledToRemove = new List<IActor>();

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

        //check for valid atbguageFilledEntries
        RemoveBrokenATBGuagesFilled();

        if (IBattleData.aTBGuagesFilled.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        display.gameObject.SetActive(true);

        attackInventoryUI = new InventoryUI();
        skillInventoryUI = new InventoryUI();
        itemInventoryUI = new InventoryUI();

        currentAllie = IBattleData.aTBGuagesFilled.First.Value;

        attackTab.onClick.RemoveAllListeners();
        magicTab.onClick.RemoveAllListeners();
        itemTab.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        attackTab.onClick.AddListener(OnClickAttack);
        magicTab.onClick.AddListener(OnClickSkill);
        itemTab.onClick.AddListener(OnClickItems);

        attackTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        magicTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;
        itemTab.GetComponent<PointerHover>().onPointerEnter = OnTabEnter;

        exitButton.onClick.AddListener(OnExit);

        commandName = "None";

        currentAllie.obj.transform.SetAsFirstSibling();
        RefreshGridWithAttackOptions();

        MenuSFXManager.Instance.PlayGenericOpen();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MarkerManager.instance.DestroyAllMarkers();
        GameStateManager.Instance.Play();
    }

    void RemoveBrokenATBGuagesFilled()
    {
        aTBGuagesFilledToRemove.Clear();

        foreach (IActor allie in IBattleData.aTBGuagesFilled)
            if (allie.obj.layer == LayerMask.NameToLayer("Allie") && allie.obj.transform.GetSiblingIndex() >= IAllie.MaxActiveAlliesCount)
                aTBGuagesFilledToRemove.Add(allie);

        foreach (IActor allie in aTBGuagesFilledToRemove)
            IBattleData.aTBGuagesFilled.Remove(allie);
    }

    void OnExit()
    {
        gameObject.SetActive(false);
    }

    public void RefreshGridWithAttackOptions()
    {
        attackInventoryUI.grid = grid;
        attackInventoryUI.buttonPrefab = buttonPrefab;
        attackInventoryUI.OnClick = OnSelectItem;
        attackInventoryUI.inventory = new Inventory();
        attackInventoryUI.onPointerEnter = OnItemEnter;
        attackInventoryUI.onPointerExit = (itemName) => { };
        attackInventoryUI.Display();
    }

    public void OnTabEnter()
    {
        MenuSFXManager.Instance.PlayHover();
    }

    public void OnItemEnter(string itemName)
    {
        MenuSFXManager.Instance.PlayHover();
    }

    public void OnClickAttack()
    {
        RefreshGridWithAttackOptions();
        OnSelectAttack(defaultAttack.name);

        MenuSFXManager.Instance.PlayClick();
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

        MenuSFXManager.Instance.PlayClick();
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

        MenuSFXManager.Instance.PlayClick();
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

        Skill item = (Skill)ItemDatabase.Instance.Get(commandName);
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
        MenuSFXManager.Instance.PlayClick();
        //MenuSFXManager.Instance.PlayGenericClose();
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
        Command command = new Command(currentAllie, (Skill)ItemDatabase.Instance.Get(commandName), target);
        IBattleData.pendingCommands.AddLast(command);

        currentAllie.getATBGuage.Reset();

        IBattleData.aTBGuagesFilled.RemoveFirst();
        gameObject.SetActive(false);

        MenuSFXManager.Instance.PlayClick();

    }
}
