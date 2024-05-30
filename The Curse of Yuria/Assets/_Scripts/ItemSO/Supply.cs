using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : Scroll, IItem
{
    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        SetDirection(user, targets);

        Global.Instance.inventories[itemType.name].Remove(name);

        user.getAnimator.UseSupply();

        foreach (IActor target in targets)
            user.StartCoroutine(performAnimation(user, target));

        yield return null;
    }

    public override IEnumerator Use(IActor target)
    {
        Global.Instance.inventories[itemType.name].Remove(name);

        target.StartCoroutine(PerformEffect(target));

        yield return null;
    }
}
