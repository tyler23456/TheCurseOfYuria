using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;

public abstract class Skill : ItemBase, IItem, ISkill
{
    [SerializeField] protected List<StatusEffectProbability> statusEffectProbabilities;
    [SerializeField] protected int power;
    [SerializeField] protected ArmTypeBase _armType;
    [SerializeField] protected ElementTypeBase _elementType;
    [SerializeField] protected CalculationTypeBase _calculationType;
    [SerializeField] protected List<BonusTypeBase> _bonusTypes;
    
    [SerializeField] protected ParticleSystem particleSystem;

    public ArmTypeBase armType { get { return _armType; } set { _armType = value; } }
    public ElementTypeBase elementType { get { return _elementType; } set { _elementType = value; } }
    public CalculationTypeBase calculationType { get { return _calculationType; } set { _calculationType = value; } }
    public List<BonusTypeBase> bonusTypes { get { return _bonusTypes; } set { _bonusTypes = value; } }

    public virtual void CheckForStatusEffectCounters(IActor user, IActor target)
    {
        foreach (string statusEffect in target.getStatusEffects.GetNames())
            StatFXDatabase.Instance.Get(statusEffect).ActivateCounter(user, target, this);
    }

    public virtual void CheckStatusEffects(IActor target)
    {
        foreach (StatusEffectProbability statusEffectProbability in statusEffectProbabilities)
            if (UnityEngine.Random.Range(0f, 1f) < statusEffectProbability.getProbability)
                statusEffectProbability.getStatusEffect.Activate(target);
    }

    public virtual bool TrueForAnyStatusEffect(Func<IStatusEffect, bool> predicate)
    {
        return statusEffectProbabilities.Find(i => predicate.Invoke(i.getStatusEffect)) != null;
    }
}
