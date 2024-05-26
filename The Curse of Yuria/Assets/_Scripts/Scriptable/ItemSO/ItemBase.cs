using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

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

    [SerializeField] protected TypeBase anyType;
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

    public int getPower => power;
    public int getCost => cost;

    public string getIdentifiers => base.name + '|' + _armType.name + '|' + _calculationType.name + '|' + _elementType.name + '|' + anyType.name;

    public ItemSprite itemSprite { get { return _itemSprite; } set { _itemSprite = value; } }
    public List<StatusEffectProbability> getStatusEffects => statusEffectProbabilities;
    public List<Modifier> getModifiers => modifiers;
    public List<Reactor> getCounters => counters;
    public List<Reactor> getInterrupts => interrupts;

    public virtual IEnumerator Use(IActor user, List<IActor> targets)
    {
        yield return null;
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
            Destroy(Instantiate(statusEffect.getVisualEffect, target.getGameObject.transform), 5f);

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