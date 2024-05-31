using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "_NewCutscene", menuName = "Cutscene/Sequencer")]
public class Sequencer : ActionBase
{
    [SerializeField] List<ActionBase> actions;

    public override IEnumerator Activate(List<IActor> actors, Image image, TMP_Text text)
    {
        foreach (ActionBase action in actions)
        {
            action.onStart = onStart;
            action.onUpdate = onUpdate;
            action.onStop = onStop;
            action.onFinish = onFinish;

            yield return action.Activate(actors, image, text);
        }
            
    }
}
