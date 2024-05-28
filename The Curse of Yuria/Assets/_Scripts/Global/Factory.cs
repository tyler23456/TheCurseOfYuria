using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using HeroEditor.Common;
using UnityEngine.AddressableAssets;
using Assets.HeroEditor.Common.Scripts.Collections;
using Assets.HeroEditor.Common.Scripts.Data;

public class Factory : MonoBehaviour
{
    public static Factory instance { get; set; }

    [SerializeField] HelmetType helmet;
    [SerializeField] EarringType earring;
    [SerializeField] GlassesType glasses;
    [SerializeField] MaskType mask;
    [SerializeField] Melee1HType melee1H;
    [SerializeField] Melee2HType melee2H;
    [SerializeField] CapeType cape;
    [SerializeField] ArmorType armor;
    [SerializeField] ShieldType shield;
    [SerializeField] BowType bow;
    [SerializeField] BasicType basic;
    [SerializeField] ScrollType scroll;
    [SerializeField] GemType gem;
    [SerializeField] QuestItemType questItem;

    [SerializeField] AssetLabelReference itemsReference;
    [SerializeField] AssetLabelReference statusEffectsReference;
    [SerializeField] AssetLabelReference partyMemberReference;
    [SerializeField] AssetLabelReference menuIconReference;

    [SerializeField] GameObject damagePopupPrefab;
    [SerializeField] GameObject recoveryPopupPrefab;

    Dictionary<string, IItem> items = new Dictionary<string, IItem>();
    Dictionary<string, IStatusEffect> statusEffects = new Dictionary<string, IStatusEffect>();
    Dictionary<string, GameObject> partyMembers = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> menuIcons = new Dictionary<string, GameObject>();

    public HelmetType getHelmet => helmet;
    public EarringType getEarring => earring;
    public GlassesType getGlasses => glasses;
    public MaskType getMask => mask;
    public Melee1HType getMelee1H => melee1H;
    public Melee2HType getMelee2H => melee2H;
    public CapeType getCape => cape;
    public ArmorType getArmor => armor;
    public ShieldType getShield => shield;
    public BowType getBow => bow;
    public BasicType getBasic => basic;
    public ScrollType getScroll => scroll;
    public GemType getGem => gem;
    public QuestItemType getQuestItem => questItem;

    public GameObject getDamagePopupPrefab => damagePopupPrefab;
    public GameObject getRecoveryPopupPrefab => recoveryPopupPrefab;

    private void Awake()
    {
        instance = this;

        Addressables.LoadAssetsAsync<IItem>(itemsReference, (i) =>
        {
            items.Add(i.name, i);
        }).WaitForCompletion();

        Addressables.LoadAssetsAsync<IStatusEffect>(statusEffectsReference, (i) =>
        {
            statusEffects.Add(i.name, i);
        }).WaitForCompletion();

        Addressables.LoadAssetsAsync<GameObject>(partyMemberReference, (i) =>
        {
            partyMembers.Add(i.name, i);
        }).WaitForCompletion();

        Addressables.LoadAssetsAsync<GameObject>(menuIconReference, (i) =>
        {
            menuIcons.Add(i.name, i);
        }).WaitForCompletion();
    }

    public IItem GetItem(string name)
    {
        return items[name];
    }

    public bool HasItem(string name)
    {
        return items.ContainsKey(name);
    }

    public IStatusEffect GetStatusEffect(string name)
    {
        return statusEffects[name];
    }

    public GameObject GetAllie(string partyMemberName)
    {
        return partyMembers[partyMemberName];
    }

    public GameObject GetMenuIcon(string menuIconName)
    {
        return menuIcons[menuIconName];
    }
}
