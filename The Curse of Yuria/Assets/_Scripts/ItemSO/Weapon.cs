using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

public class Weapon : Equipable, IItem, IWeapon, IEquipment
{
    [SerializeField] protected List<StatusEffectProbability> statusEffectProbabilities;
    [SerializeField] protected float power = 2f;
    [SerializeField] protected ArmTypeBase _armType;
    [SerializeField] protected ElementTypeBase _elementType;
    [SerializeField] protected CalculationTypeBase _calculationType;
    [SerializeField] protected List<BonusTypeBase> _bonusTypes;
    [SerializeField] protected ParticleSystem particleSystem;

    public ArmTypeBase armType { get { return _armType; } set { _armType = value; } }
    public ElementTypeBase elementType { get { return _elementType; } set { _elementType = value; } }
    public CalculationTypeBase calculationType { get { return _calculationType; } set { _calculationType = value; } }
    public List<BonusTypeBase> bonusTypes { get { return _bonusTypes; } set { _bonusTypes = value; } }

    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        SetDirection(user, targets);

        Animator animator = user.obj.GetComponent<Animator>();
        animator?.SetTrigger("Slash");

        foreach (IActor target in targets)
            target.StartCoroutine(performAnimation(user, target));

        yield return null;
    }

    protected virtual IEnumerator performAnimation(IActor user, IActor target)
    {
        yield return new WaitForSeconds(0.5f);
        yield return PerformEffect(user, target);
    }

    protected virtual IEnumerator PerformEffect(IActor user, IActor target)
    {
        if (user == null)
            yield break;

        float accumulator = 0;
        accumulator = _elementType.Calculate(user, target, power * IStats.powerMultiplier);
        accumulator = _armType.Calculate(user, target, accumulator);

        foreach (BonusTypeBase bonusType in _bonusTypes)
            accumulator = bonusType.Calculate(user, target, accumulator);

        accumulator = _calculationType.Calculate(user, target, accumulator);

        CheckStatusEffects(target);
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
}