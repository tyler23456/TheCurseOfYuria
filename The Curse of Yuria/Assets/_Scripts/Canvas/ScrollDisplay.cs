public class ScrollDisplay : ItemDisplayBase
{
    public static ScrollDisplay Instance { get; protected set; }

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        localInventoryUI = new InventoryUI();
        globalInventoryUI = new InventoryUI();

        allieIndex = 0;
        RefreshGlobalEquipment(InventoryManager.Instance.scrolls);
        RefreshAllieEquipment();

        AudioManager.Instance.PlaySFX(open);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        allie?.obj.SetActive(previousActive);
        AudioManager.Instance.PlaySFX(close);
    }

    protected override void RefreshAllieGeneric(int offset = 0)
    {
        base.RefreshAllieEquipment(offset);

        globalInventory = allie.getScrolls;

        int[] statValues = stats.GetAttributes();
        for (int i = 0; i < statValues.Length; i++)
        {
            allieStats.text += ((IStats.Attribute)i).ToString() + "\n";
            allieValues.text += statValues[i].ToString() + "\n";
        }
    }

    protected void OnClickAllieItem(string itemName)
    {
        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Unequip(allie);

        InventoryManager.Instance.scrolls.Add(itemName);

        RefreshAllieEquipment();
        RefreshLocalScrolls(allie.getScrolls);
        RefreshGlobalEquipment(InventoryManager.Instance.scrolls);
        ClearItemAndAllieData();
    }

    protected void OnEquipEquipment(string itemName)
    {
        if (globalInventory.Contains(itemName))
            return;

        InventoryManager.Instance.scrolls.Remove(itemName);

        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Equip(allie);

        RefreshAllieEquipment();
        RefreshLocalScrolls(allie.getScrolls);
        RefreshGlobalEquipment(InventoryManager.Instance.scrolls);
        ClearItemAndAllieData();
    }

    protected override void OnEnterGlobalEquipment(string itemName)
    {
        if (globalInventory.Contains(itemName))
            return;

        IItem scroll = ItemDatabase.Instance.Get(itemName);

        this.itemName.text = itemName;
        this.itemInfo.text = scroll.getInfo;
        this.allieIncreases.text = "";
        this.itemSprite.sprite = scroll.icon;
    }
}
