using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewMover", menuName = "Cutscene/Mover")]
public class Mover : ActionBase, ICutsceneAction
{
    [SerializeField] string actorName = "default";
    [SerializeField] Vector2 actorDestination;

    public override IEnumerator Activate()
    {
        base.Activate();
        yield return null;
    }
}
