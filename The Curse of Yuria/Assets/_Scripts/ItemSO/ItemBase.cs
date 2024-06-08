using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using UnityEditor;

public abstract class ItemBase : TypeBase
{
    [SerializeField] [HideInInspector] protected ulong guid;

    [SerializeField] protected Sprite _icon;
    [SerializeField] protected GameObject _prefab;
    [SerializeField] protected ItemTypeBase _equipmentType;
    [SerializeField] [TextArea(3, 10)] protected string info;

    [SerializeField] protected ArmTypeBase _armType;
    [SerializeField] protected ElementTypeBase _elementType;
    [SerializeField] protected CalculationTypeBase _calculationType;
    [SerializeField] protected List<BonusTypeBase> _bonusTypes;

    [SerializeField] protected int power;
    [SerializeField] protected int cost;

    [SerializeField] List<GameObject> effects;
    [SerializeField] protected ParticleSystem particleSystem;

    [SerializeField] protected ItemSprite _itemSprite;
    [SerializeField] protected List<StatusEffectProbability> statusEffectProbabilities;
    [SerializeField] protected List<Modifier> modifiers;
    [SerializeField] protected List<Reactor> counters; 
    [SerializeField] protected List<Reactor> interrupts;

    public ulong getGuid => guid;
    public Sprite icon { get { return _icon; } set { _icon = value; } }
    public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
    public ItemTypeBase itemType { get { return _equipmentType; } set { _equipmentType = value; } }
    public string getInfo => info;

    public ArmTypeBase armType { get { return _armType; } set { _armType = value; } }
    public ElementTypeBase elementType { get { return _elementType; } set { _elementType = value; } }
    public CalculationTypeBase calculationType { get { return _calculationType; } set { _calculationType = value; } }
    public List<BonusTypeBase> bonusTypes { get { return _bonusTypes; } set { _bonusTypes = value; } }

    public int getPower => power;
    public int getCost => cost;

    public string getIdentifiers => base.name + '|' + _armType.name + '|' + _calculationType.name + '|' + _elementType.name;

    public ItemSprite itemSprite { get { return _itemSprite; } set { _itemSprite = value; } }
    public List<StatusEffectProbability> getStatusEffects => statusEffectProbabilities;
    public List<Modifier> getModifiers => modifiers;
    public List<Reactor> getCounters => counters;
    public List<Reactor> getInterrupts => interrupts;

    public virtual IEnumerator Use(IActor user, List<IActor> targets)
    {
        yield return null;
    }

    protected void SetDirection(IActor user, List<IActor> targets)
    {
        if (targets.Count == 0)
            return;

        Vector2 direction = (targets[0].obj.transform.position - user.obj.transform.position).normalized;

        if (direction.x >= 0)
            user.obj.transform.GetChild(0).eulerAngles = new Vector3(0f, 0f, 0f);
        else
            user.obj.transform.GetChild(0).eulerAngles = new Vector3(0f, 180f, 0f);
    }

    public virtual IEnumerator Use(IActor target)
    {
        yield return null;
    }

    public virtual void CheckStatusEffects(IActor target)
    {
        foreach (StatusEffectProbability statusEffectProbability in statusEffectProbabilities)
            if (Random.Range(0f, 1f) > statusEffectProbability.getProbability)
                ApplyStatusEffect(target, statusEffectProbability.getStatusEffect);
    }

    public virtual void ApplyStatusEffect(IActor target, StatusEffectBase statusEffect)
    {
        if (statusEffect.getVisualEffect != null)
            Destroy(Instantiate(statusEffect.getVisualEffect, target.obj.transform), 5f);

        statusEffect.Activate(target);
    }

    public virtual void Equip(IActor target)
    {
        foreach (Modifier modifier in modifiers)
            target.getStats.OffsetAttribute(modifier.getAttribute, modifier.getOffset);

        foreach (Reactor counter in counters)
            target.getCounters.Add(counter);

        foreach (Reactor interrupt in interrupts)
            target.getInterrupts.Add(interrupt);
    }

    public virtual void Unequip(IActor target)
    {
        foreach (Modifier modifier in modifiers)
            target.getStats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);

        foreach (Reactor counter in counters)
            target.getCounters.Remove(counter);

        foreach (Reactor interrupt in interrupts)
            target.getInterrupts.Remove(interrupt);
    }
}

