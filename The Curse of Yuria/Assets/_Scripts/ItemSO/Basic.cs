using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : Skill, IItem
{
    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        SetDirection(user, targets);

        InventoryManager.Instance.basic.Remove(name);

        user.obj.GetComponent<Animator>()?.SetTrigger("UseSupply");

        foreach (IActor target in targets)
            user.StartCoroutine(performAnimation(user, target));

        yield return null;
    }

    public override IEnumerator Use(IActor target)
    {
        //InventoryManager.Instance.basic.Remove(name);

        target.StartCoroutine(PerformEffect(target));

        yield return null;
    }

    protected virtual IEnumerator performAnimation(IActor user, IActor target)
    {
        yield return new WaitForSeconds(0.5f);
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
}
