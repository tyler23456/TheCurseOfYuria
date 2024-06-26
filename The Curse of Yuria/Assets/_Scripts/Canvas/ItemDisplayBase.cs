using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Linq;
using System.Collections.ObjectModel;


public class ItemDisplayBase : DisplayBase
{
    [Header("Prefabs")]
    [SerializeField] protected Button buttonPrefab;
    [SerializeField] protected Sprite emptySprite;

    [Header("SFX")]
    [SerializeField] protected AudioClip open; // will eventually become scriptable objects
    [SerializeField] protected AudioClip close;
    [SerializeField] protected AudioClip equip;
    [SerializeField] protected AudioClip unequip;
    [SerializeField] protected AudioClip cyclePartyMembers;
    [SerializeField] protected AudioClip cycleEquipmentParts;

    [Header("Camera And Grids")]
    [SerializeField] protected Camera detailedActorViewCamera;
    [SerializeField] protected RectTransform globalInventoryGrid;
    [SerializeField] protected RectTransform allieInventoryGrid;

    [Header("Allie Info")]
    [SerializeField] protected Text allieName;
    [SerializeField] protected Text allieStats;
    [SerializeField] protected Text allieValues;
    [SerializeField] protected Text allieIncreases;

    [Header("Item Info")]
    [SerializeField] protected Text itemName;
    [SerializeField] protected Image itemSprite;
    [SerializeField] protected Text itemInfo;
    
    protected ReadOnlyCollection<Modifier> oldModifiers;
    protected ReadOnlyCollection<Modifier> newModifiers;
    protected Modifier oldModifier;
    protected Modifier newModifier;
    protected int oldModifierValue = 0;
    protected int newModifierValue = 0;
    protected int modifierValue = 0;

    protected bool previousActive = true;

    protected InventoryUI allieInventoryUI;
    protected InventoryUI globalInventoryUI;

    protected int allieIndex = 0;
    protected IActor allie;
    protected IStats stats;
    protected IEquipment previousItem;
    protected IEquipment currentItem;
    protected IInventory globalInventory;
    protected ItemTypeBase currentType;

    public virtual void RefreshAllie(int offset)
    {
        allie?.obj.SetActive(previousActive);

        allieIndex += offset;
        allieIndex = Mathf.Clamp(allieIndex, 0, AllieManager.Instance.count - 1);

        allie = AllieManager.Instance[allieIndex];
        previousActive = allie.obj.activeSelf;
        allie.obj.SetActive(true);

        detailedActorViewCamera.cullingMask = (1 << allie.obj.transform.GetChild(0).gameObject.layer)
            | (1 << LayerMask.NameToLayer("Light"));

        stats = allie.getStats;

        allieName.text = allie.obj.name;
        allieStats.text = "";
        allieValues.text = "";
    }

    public void RefreshAllieInventory(IInventory inventory, bool showName = false, bool showCount = true, bool showSprite = true)
    {
        allieInventoryUI.showName = showName;
        allieInventoryUI.showCount = showCount;
        allieInventoryUI.showSprite = showSprite;
        allieInventoryUI.grid = allieInventoryGrid;
        allieInventoryUI.buttonPrefab = buttonPrefab;
        allieInventoryUI.inventory = inventory;
        allieInventoryUI.OnClick = OnClickAllieItem;
        allieInventoryUI.onPointerEnter = OnEnterAllieItem;
        allieInventoryUI.onPointerExit = OnExitAllieItem;
        allieInventoryUI.Display();
    }


    public void RefreshGlobalInventory(IInventory inventory, bool showName = false, bool showCount = true, bool showSprite = true)
    {
        globalInventoryUI.showName = showName;
        globalInventoryUI.showCount = showCount;
        globalInventoryUI.showSprite = showSprite;
        globalInventoryUI.grid = globalInventoryGrid;
        globalInventoryUI.buttonPrefab = buttonPrefab;
        globalInventoryUI.inventory = inventory;
        globalInventoryUI.OnClick = OnClickGlobalItem;
        globalInventoryUI.onPointerEnter = OnEnterGlobalItem;
        globalInventoryUI.onPointerExit = OnExitGlobalItem;
        globalInventoryUI.Display();
    }

    public virtual void Update()
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

    public void ClearItemAndAllieData()
    {
        itemName.text = "";
        itemInfo.text = "";
        allieIncreases.text = "";
        itemSprite.sprite = emptySprite;
    }

    public virtual void OnClickAllieItem(string itemName)
    {

    }

    public virtual void OnEnterAllieItem(string itemName)
    {

    }

    public virtual void OnExitAllieItem(string itemName)
    {
      
    }

    public virtual void OnClickGlobalItem(string itemName)
    {

    }

    public virtual void OnEnterGlobalItem(string itemName)
    {

    }

    public virtual void OnExitGlobalItem(string itemName)
    {

    }
}

