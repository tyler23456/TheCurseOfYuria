using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Collections.ObjectModel;

public class ScrollDisplay : DisplayBase
{
    public static DisplayBase Instance { get; protected set; }

    [SerializeField] Button buttonPrefab;

    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;
    [SerializeField] AudioClip equip;
    [SerializeField] AudioClip unequip;
    [SerializeField] AudioClip cyclePartyMembers;

    [SerializeField] Camera detailedActorViewCamera;

    [SerializeField] RectTransform inventoryGrid;
    [SerializeField] RectTransform partyMemberGrid;

    [SerializeField] Text itemName;
    [SerializeField] Image itemSprite;
    [SerializeField] Text itemInfo;

    [SerializeField] Text partyMemberName;
    [SerializeField] Text partyMemberStats;
    [SerializeField] Text partyMemberValues;
    [SerializeField] Text partyMemberIncreases;

    [SerializeField] Sprite emptySprite;

    int allieIndex = 0;

    IActor allie;

    IInventory skills;
    IStats stats;

    IItem newSkill;

    InventoryUI partyMemberInventoryUI;
    InventoryUI globalInventoryUI;

    bool previousActive = true;

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        partyMemberInventoryUI = new InventoryUI();
        globalInventoryUI = new InventoryUI();

        allieIndex = 0;
        RefreshInventoryScrolls();
        RefreshAllie();

        AudioManager.Instance.PlaySFX(open);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        allie?.obj.SetActive(previousActive);
        AudioManager.Instance.PlaySFX(close);
    }

    public void RefreshInventoryScrolls()
    {
        globalInventoryUI.displayName = true;
        globalInventoryUI.grid = inventoryGrid;
        globalInventoryUI.buttonPrefab = buttonPrefab;
        globalInventoryUI.inventory = InventoryManager.Instance.scrolls;
        globalInventoryUI.OnClick = (itemName) => OnAddSkill(itemName);
        globalInventoryUI.onPointerEnter = (itemName) => OnPointerEnterInventoryIcon(itemName);
        globalInventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
        globalInventoryUI.Display();
    }

    public void RefreshAllie(int offset = 0)
    {
        allie?.obj.SetActive(previousActive);

        allieIndex += offset;
        allieIndex = Mathf.Clamp(allieIndex, 0, AllieManager.Instance.count - 1);

        allie = AllieManager.Instance[allieIndex];
        previousActive = allie.obj.activeSelf;
        allie.obj.SetActive(true);

        detailedActorViewCamera.cullingMask = (1 << allie.obj.transform.GetChild(0).gameObject.layer) 
            | ( 1 << LayerMask.NameToLayer("Light"));

        skills = allie.getScrolls;
        stats = allie.getStats;

        //show party member stuff

        partyMemberName.text = allie.obj.name;
        partyMemberStats.text = "";
        partyMemberValues.text = "";

        int[] statValues = stats.GetAttributes();
        for (int i = 0; i < statValues.Length; i++)
        {
            partyMemberStats.text += ((IStats.Attribute)i).ToString() + "\n";
            partyMemberValues.text += statValues[i].ToString() + "\n";
        }
        partyMemberInventoryUI.displayName = true;
        partyMemberInventoryUI.displayCount = false;
        partyMemberInventoryUI.grid = partyMemberGrid;
        partyMemberInventoryUI.buttonPrefab = buttonPrefab;
        partyMemberInventoryUI.inventory = allie.getScrolls;
        partyMemberInventoryUI.OnClick = (itemName) => OnRemoveSkill(itemName);
        partyMemberInventoryUI.onPointerEnter = (itemName) => OnPointerEnterPartyMemberSkillsIcon(itemName);
        partyMemberInventoryUI.onPointerExit = (itemName) => OnPointerExit(itemName);
        partyMemberInventoryUI.Display();
    }
    public void OnAddSkill(string itemName)
    {
        if (skills.Contains(itemName))
            return;

        InventoryManager.Instance.scrolls.Remove(itemName);

        newSkill = ItemDatabase.Instance.Get(itemName);
        newSkill.Equip(allie);

        RefreshAllie();
        RefreshInventoryScrolls();
    }

    public void OnRemoveSkill(string itemName)
    {
        newSkill = ItemDatabase.Instance.Get(itemName);
        newSkill.Unequip(allie);

        InventoryManager.Instance.scrolls.Add(itemName);

        RefreshAllie();
        RefreshInventoryScrolls();
    }

    public void OnPointerEnterInventoryIcon(string itemName)
    {
        if (skills.Contains(itemName))
            return;

        newSkill = ItemDatabase.Instance.Get(itemName);

        this.itemName.text = itemName;
        this.itemInfo.text = newSkill.getInfo;
        partyMemberIncreases.text = "";
        this.itemSprite.sprite = newSkill.icon;
    }

    public void OnPointerEnterPartyMemberSkillsIcon(string itemName)
    {
        if (skills.Contains(itemName))
            return;

        newSkill = ItemDatabase.Instance.Get(itemName);

        this.itemName.text = itemName;
        this.itemInfo.text = newSkill.getInfo;
        partyMemberIncreases.text = "";
        this.itemSprite.sprite = newSkill.icon;
    }

    public void OnPointerExit(string itemName)
    {
        this.itemName.text = "";
        itemInfo.text = "";
        partyMemberIncreases.text = "";
        this.itemSprite.sprite = emptySprite;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {          
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshAllie(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshAllie(1);
        }

        detailedActorViewCamera.transform.position = allie.obj.transform.position + new Vector3(0f, 1f, -2.5f);
    }
}
