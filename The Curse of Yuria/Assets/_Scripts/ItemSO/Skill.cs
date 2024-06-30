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

    protected virtual bool CheckForStatusEffectCounters(IActor user, IActor target)
    {
        List<bool> itemCancellationFlags = new List<bool>();
        foreach (string statusEffect in target.getStatusEffects.GetNames())
            itemCancellationFlags.Add(StatFXDatabase.Instance.Get(statusEffect).OnHit(user, target, this));

        if (itemCancellationFlags.Contains(true))
            return true;

        return false;
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

    public bool ContainsStatusEffectThatCanRemoveKO()
    {
        return TrueForAnyStatusEffect(i => i is IRestoration && ((IRestoration)i).ContainsStatusEffectToRemove(StatFXDatabase.Instance.getKnockOut.name));
    }

    public bool IsInvalidTarget(IActor target)
    {
        return target.enabled == false && !ContainsStatusEffectThatCanRemoveKO() || target.obj.activeSelf == false;
    }
}
