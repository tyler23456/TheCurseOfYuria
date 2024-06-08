using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;


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
    List<Modifier> newModifiers;
    Modifier newModifier;
    int modifierValue = 0;

    InventoryUI partyMemberInventoryUI;
    InventoryUI globalInventoryUI;

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
        RefreshPartyMember();

        AudioManager.Instance.PlaySFX(open);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

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

    public void RefreshPartyMember()
    {
        allie = AllieManager.Instance[allieIndex];

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

        RefreshPartyMember();
        RefreshInventoryScrolls();
    }

    public void OnRemoveSkill(string itemName)
    {
        newSkill = ItemDatabase.Instance.Get(itemName);
        newSkill.Unequip(allie);

        InventoryManager.Instance.scrolls.Add(itemName);

        RefreshPartyMember();
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

        newModifiers = newSkill.getModifiers;

        int length = AllieManager.Instance[allieIndex].getStats.GetAttributes().Length;

        for (int i = 0; i < length; i++)
        {
            newModifier = newModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);

            if (newModifier == null)
                modifierValue = 0;
            else
                modifierValue = newModifier.getOffset;

            if (modifierValue == 0)
                partyMemberIncreases.text += "<color=#555555ff>" + modifierValue.ToString() + "\n" + "</color>";
            else if (modifierValue > 0)
                partyMemberIncreases.text += "<color=#00ff00ff>" + "+ " + modifierValue.ToString() + "\n" + "</color>";
            else
                partyMemberIncreases.text += "<color=#ff0000ff>" + "" + modifierValue.ToString() + "\n" + "</color>";
        }
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

        newModifiers = newSkill.getModifiers;

        int length = AllieManager.Instance[allieIndex].getStats.GetAttributes().Length;

        for (int i = 0; i < length; i++)
        {
            newModifier = newModifiers.FirstOrDefault(e => e.getAttribute == (IStats.Attribute)i);

            if (newModifier == null)
                modifierValue = 0;
            else
                modifierValue = newModifier.getOffset;

            if (modifierValue == 0)
                partyMemberIncreases.text += "<color=#555555ff>" + modifierValue.ToString() + "\n" + "</color>";
            else if (modifierValue > 0)
                partyMemberIncreases.text += "<color=#00ff00ff>" + "+ " + modifierValue.ToString() + "\n" + "</color>";
            else
                partyMemberIncreases.text += "<color=#ff0000ff>" + "" + modifierValue.ToString() + "\n" + "</color>";
        }
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
            allieIndex--;
            allieIndex = Mathf.Clamp(allieIndex, 0, AllieManager.Instance.count - 1);
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshPartyMember();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            allieIndex++;
            allieIndex = Mathf.Clamp(allieIndex, 0, AllieManager.Instance.count - 1);
            AudioManager.Instance.PlaySFX(cyclePartyMembers);
            RefreshPartyMember();
        }

        detailedActorViewCamera.transform.position = allie.obj.transform.position + new Vector3(0f, 1f, -2.5f);
    }
}
