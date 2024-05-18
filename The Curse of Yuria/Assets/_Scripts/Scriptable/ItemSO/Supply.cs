using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : Scroll, IItem
{
    public override IEnumerator Use(IActor user, IActor[] targets)
    {
        global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

        global.inventories[IItem.Category.supplies].Remove(name);

        user.getAnimator.UseSupply();

        foreach (IActor target in targets)
            user.StartCoroutine(performAnimation(user, target));

        yield return null;
    }

    public override IEnumerator Use(IActor target)
    {
        global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

        global.inventories[IItem.Category.supplies].Remove(name);

        target.StartCoroutine(PerformEffect(target));

        yield return null;
    }
}
