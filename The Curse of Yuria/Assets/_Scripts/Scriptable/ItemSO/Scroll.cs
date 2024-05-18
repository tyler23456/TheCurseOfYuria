using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scroll : ItemBase, IItem
{
    public override IEnumerator Use(IActor user, IActor[] targets)
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

        while (particleSystem != null)
            yield return new WaitForEndOfFrame();

        if (user == null)
            yield break;

        target.getStats.ApplyCalculation(power, user.getStats, group, type, element);
        CheckStatusEffects(target);
        global.successfulSubcommands.Add(new Subcommand(user, this, target));
    }

    protected virtual IEnumerator PerformEffect(IActor target)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.getGameObject.transform).GetComponent<ParticleSystem>();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        while (particleSystem != null)
            yield return new WaitForEndOfFrame();

        if (target == null)
            yield break;

        target.getStats.ApplyCalculation(power, element);
    }

    public override void Equip(IActor target)
    {
        base.Equip(target);

        if (target.getSkills.Contains(itemName))
            return;

        target.getSkills.Add(itemName);
    }

    public override void Unequip(IActor target)
    {
        base.Unequip(target);
        target.getSkills.Remove(itemName);
    }


}
