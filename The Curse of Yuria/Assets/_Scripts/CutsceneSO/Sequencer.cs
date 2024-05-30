using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_NewCutscene", menuName = "Cutscene/Sequencer")]
public class Sequencer : ActionBase
{
    [SerializeField] List<ActionBase> actions;

    public void Start()
    {
        List<IActor> actors = new List<IActor>();
        Global.Instance.StartCoroutine(Activate(actors));
    }

    public override IEnumerator Activate(List<IActor> actors)
    {
        foreach (ActionBase action in actions)
        {
            action.onStart = onStart;
            action.onUpdate = onUpdate;
            action.onStop = onStop;
            action.onFinish = onFinish;

            yield return action.Activate(actors);
        }
            
    }
}
