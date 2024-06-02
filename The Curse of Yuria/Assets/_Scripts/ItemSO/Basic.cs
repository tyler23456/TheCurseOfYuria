using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : Scroll, IItem
{
    public override IEnumerator Use(IActor user, List<IActor> targets)
    {
        SetDirection(user, targets);

        InventoryManager.Instance.basic.Remove(name);

        user.getAnimator.UseSupply();

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
}
