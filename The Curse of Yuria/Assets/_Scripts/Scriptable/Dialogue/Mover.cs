using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : ActionBase, IAction
{
    [SerializeField] string actorName = "default";
    [SerializeField] Vector2 actorDestination;

    public override IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        base.Activate(global, factory, actors);

        

        yield return null;
    }
}
