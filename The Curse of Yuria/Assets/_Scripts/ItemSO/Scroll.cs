using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scroll : Skill, IItem, IScroll
{
    [SerializeField] protected int cost;

    public int getCost => cost;

    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        LightManager.instance.FadeOut();

        SetDirection(user, targets);

        user.getStats.ApplyCost(cost);
        user.obj.GetComponent<Animator>()?.SetTrigger("Cast");

        foreach (IActor target in targets)
            target.StartCoroutine(performAnimation(user, target));

        yield return null;
    }

    public override IEnumerator Use(IActor target)
    {
        target.StartCoroutine(PerformEffect(target));
        yield return null;
    }

    protected virtual IEnumerator performAnimation(IActor user, IActor target)
    {
        yield return new WaitForSeconds(0.5f);
        CheckForStatusEffectCounters(user, target);
        yield return PerformEffect(user, target);
    }

    protected virtual IEnumerator PerformEffect(IActor user, IActor target)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.obj.transform).GetComponent<ParticleSystem>();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        while (particleSystem.time < particleSystem.main.duration / 10f)
            yield return new WaitForEndOfFrame();

        if (user == null)
            yield break;

        float accumulator = 0;
        accumulator = _elementType.Calculate(user, target, power * IStats.powerMultiplier);
        accumulator = _armType.Calculate(user, target, accumulator);

        foreach (BonusTypeBase bonusType in _bonusTypes)
            accumulator = bonusType.Calculate(user, target, accumulator);

        accumulator = _calculationType.Calculate(user, target, accumulator);

        CheckStatusEffects(target);

        LightManager.instance.FadeIn();
    }

    protected virtual IEnumerator PerformEffect(IActor target)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.obj.transform).GetComponent<ParticleSystem>();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        while (particleSystem.time < particleSystem.main.duration / 10f)
            yield return new WaitForEndOfFrame();

        if (target == null)
            yield break;

        float accumulator = 0;
        accumulator = _elementType.Calculate(null, target, power * IStats.powerMultiplier);
        accumulator = _calculationType.Calculate(null, target, accumulator);
    }

    public override void Equip(IActor target)
    {
        base.Equip(target);

        if (target.getScrolls.Contains(name))
            return;

        target.getScrolls.Add(name);
    }

    public override void Unequip(IActor target)
    {
        base.Unequip(target);
        target.getScrolls.Remove(name);
    }


}
