using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : EquipmentDisplay
{
    public static ShopDisplay Instance { get; protected set; }

    [Header("Shop")]
    [SerializeField] Button buy;
    [SerializeField] Button sell;
    [SerializeField] Text selectedInfo;
    [SerializeField] Text moneyLabel;
    [SerializeField] Text moneyValue;
    [SerializeField] Text moneyIncrease;

    bool isBuying = true;
    Dictionary<ItemTypeBase, Inventory> shopInventories = new Dictionary<ItemTypeBase, Inventory>();

    ScriptableObject shopDate;

    //will need access to a scriptable object

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        buy.onClick.AddListener(OnClickBuy);
        sell.onClick.AddListener(OnClickSell);

        OnClickBuy();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

    }

    public void ShowExclusivelyInParent(ScriptableObject shopData)
    {

    }

    protected void OnClickBuy()
    {
        isBuying = true;
    }

    protected void OnClickSell()
    {
        isBuying = false;
    }

    protected override void OnUnequip(ItemTypeBase type)
    {
        
    }

    protected override void OnClickGlobalItem(string itemName)
    {
        
    }

    protected override void OnEnterGlobalItem(string itemName)
    {
        base.OnEnterGlobalItem(itemName);
    }

    protected override void OnExitGlobalItem(string itemName)
    {
        base.OnExitGlobalItem(itemName);
    }
}
