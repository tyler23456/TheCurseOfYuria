using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewMover", menuName = "Cutscene/Mover")]
public class Mover : ActionBase, IAction
{
    [SerializeField] string actorName = "default";
    [SerializeField] Vector2 actorDestination;

    public override IEnumerator Activate(List<IActor> actors, Image image, TMP_Text text)
    {
        base.Activate(actors, image, text);
        yield return null;
    }
}
