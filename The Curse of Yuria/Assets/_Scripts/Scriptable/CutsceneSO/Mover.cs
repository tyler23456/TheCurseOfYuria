using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMover", menuName = "Cutscene/Mover")]
public class Mover : ActionBase, IAction
{
    [SerializeField] string actorName = "default";
    [SerializeField] Vector2 actorDestination;

    public override IEnumerator Activate(List<IActor> actors)
    {
        base.Activate(actors);

        

        yield return null;
    }
}
