using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scroll : ItemBase, IItem
{
    public override void Use(IActor user, IActor[] targets)
    {
        if (user.getStats.MP - cost < 0)
            return; //might need a display message letting the player know the user does not have enough mp

        user.getStats.MP -= cost;

        IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
        user.getAnimator.Cast();
        global.StartCoroutine(performAnimation(user, targets));
    }

    public override void Use(IActor[] targets)
    {
        IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

        foreach (IActor target in targets)
            global.StartCoroutine(PerformEffect(target));
    }

    protected virtual IEnumerator performAnimation(IActor user, IActor[] targets)
    {
        yield return new WaitForSeconds(0.5f);

        foreach (IActor target in targets)
            yield return PerformEffect(user, target);  //may need to start a new coroutine for this? 
    }

    protected virtual IEnumerator PerformEffect(IActor user, IActor target)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.getGameObject.transform).GetComponent<ParticleSystem>();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        while (particleSystem != null)
            yield return new WaitForEndOfFrame();

        target.getStats.ApplySkillCalculation(power, user.getStats, group, type, element);
        CheckStatusEffects(target);
    }

    protected virtual IEnumerator PerformEffect(IActor target)
    {
        ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.getGameObject.transform).GetComponent<ParticleSystem>();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

        while (particleSystem != null)
            yield return new WaitForEndOfFrame();

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
