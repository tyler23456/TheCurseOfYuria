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

        allieInventoryUI = new InventoryUI();
        globalInventoryUI = new InventoryUI();

        allieIndex = 0;
        RefreshGlobalInventory(InventoryManager.Instance.scrolls, showName: true);
        RefreshAllie();

        AudioManager.Instance.PlaySFX(open);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        allie?.obj.SetActive(previousActive);
        AudioManager.Instance.PlaySFX(close);
    }

    protected override void RefreshAllie(int offset = 0)
    {
        base.RefreshAllie(offset);

        globalInventory = allie.getScrolls;

        int[] statValues = stats.GetAttributes();
        for (int i = 0; i < statValues.Length; i++)
        {
            allieStats.text += ((IStats.Attribute)i).ToString() + "\n";
            allieValues.text += statValues[i].ToString() + "\n";
        }
    }

    protected override void OnClickAllieItem(string itemName)
    {
        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Unequip(allie);

        InventoryManager.Instance.scrolls.Add(itemName);

        RefreshAllie();
        RefreshAllieInventory(allie.getScrolls, showName: true, showCount: false);
        RefreshGlobalInventory(InventoryManager.Instance.scrolls, showName: true);
        ClearItemAndAllieData();
    }

    protected override void OnClickGlobalItem(string itemName)
    {
        if (globalInventory.Contains(itemName))
            return;

        InventoryManager.Instance.scrolls.Remove(itemName);

        IItem scroll = ItemDatabase.Instance.Get(itemName);
        scroll.Equip(allie);

        RefreshAllie();
        RefreshAllieInventory(allie.getScrolls, showName: true, showCount: false);
        RefreshGlobalInventory(InventoryManager.Instance.scrolls, showName: true);
        ClearItemAndAllieData();
    }

    protected override void OnEnterGlobalItem(string itemName)
    {
        if (globalInventory.Contains(itemName))
            return;

        IItem scroll = ItemDatabase.Instance.Get(itemName);

        this.itemName.text = itemName;
        this.itemInfo.text = scroll.getInfo;
        this.allieIncreases.text = "";
        this.itemSprite.sprite = scroll.icon;
    }

    protected override void OnExitGlobalItem(string itemName)
    {
        ClearItemAndAllieData();
    }
}
