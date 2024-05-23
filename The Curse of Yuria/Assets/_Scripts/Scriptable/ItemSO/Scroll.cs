using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scroll : ItemBase, IItem
{
    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

        user.getStats.MP -= cost;
        user.getAnimator.Cast();

        foreach (IActor target in targets)
            target.StartCoroutine(performAnimation(user, target));

        yield return null;
    }

    public override IEnumerator Use(IActor target)
    {
        global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

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
        ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.getGameObject.transform).GetComponent<ParticleSystem>();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        while (particleSystem.time < particleSystem.main.duration / 10f)
            yield return new WaitForEndOfFrame();

        if (user == null)
            yield break;

        float accumulator = 0;
        accumulator = _elementType.Calculate(user, target, accumulator);
        accumulator = _armType.Calculate(user, target, accumulator);
        accumulator = _calculationType.Calculate(user, target, accumulator);

        CheckStatusEffects(target);
    }

    protected virtual IEnumerator PerformEffect(IActor target)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.getGameObject.transform).GetComponent<ParticleSystem>();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        while (particleSystem.time < particleSystem.main.duration / 10f)
            yield return new WaitForEndOfFrame();

        if (target == null)
            yield break;

        float accumulator = 0;
        accumulator = _elementType.Calculate(null, target, accumulator);
        accumulator = _calculationType.Calculate(null, target, accumulator);
    }

    public override void Equip(IActor target)
    {
        base.Equip(target);

        if (target.getSkills.Contains(name))
            return;

        target.getSkills.Add(name);
    }

    public override void Unequip(IActor target)
    {
        base.Unequip(target);
        target.getSkills.Remove(name);
    }


}
