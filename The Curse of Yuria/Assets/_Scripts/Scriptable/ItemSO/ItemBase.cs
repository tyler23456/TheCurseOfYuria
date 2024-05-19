using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

public abstract class ItemBase : ScriptableObject
{
    protected static IGlobal global;

    [SerializeField] [HideInInspector] protected ulong guid;

    [SerializeField] protected Sprite _icon;
    [SerializeField] protected GameObject _prefab;
    [SerializeField] protected IItem.Category _category;
    [SerializeField] [TextArea(3, 10)] protected string info;

    [SerializeField] protected IItem.Group group = IItem.Group.None;
    [SerializeField] protected IItem.Type type = IItem.Type.None;
    [SerializeField] protected IItem.Element element = IItem.Element.None;
    [SerializeField] protected int power;
    [SerializeField] protected int cost;
    [SerializeField] protected float duration = float.PositiveInfinity;
    [SerializeField] List<GameObject> effects;
    [SerializeField] protected ParticleSystem particleSystem;

    [SerializeField] protected ItemSprite _itemSprite;
    [SerializeField] protected List<StatusEffectProbability> statusEffectProbabilities;
    [SerializeField] protected List<Modifier> modifiers;
    [SerializeField] protected List<Reactor> counters; 
    [SerializeField] protected List<Reactor> interrupts;

    public ulong getGuid => guid;
    public new string name { get { return base.name; } set { base.name = value; } }
    public Sprite icon { get { return _icon; } set { _icon = value; } }
    public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
    public IItem.Category category { get { return _category; } set { _category = value; } }
    public string getInfo => info;

    public IItem.Group getGroup => group;
    public IItem.Type getType => type;
    public IItem.Element getElement => element;
    public int getPower => power;
    public int getCost => cost;
    public float getDuration => duration;

    public string getIdentifiers => base.name + '|' + group.ToString() + '|' + type.ToString() + '|' + element.ToString();

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
            target.counters.Add(counter);

        foreach (Reactor interrupt in interrupts)
            target.interrupts.Add(interrupt);
    }

    public virtual void Unequip(IActor target)
    {
        foreach (Modifier modifier in modifiers)
            target.getStats.OffsetAttribute(modifier.getAttribute, -modifier.getOffset);

        foreach (Reactor counter in counters)
            target.counters.Remove(counter);

        foreach (Reactor interrupt in interrupts)
            target.interrupts.Remove(interrupt);
    }
}