using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_NewCutscene", menuName = "Cutscene/Sequencer")]
public class Sequencer : ActionBase
{
    IGlobal global;
    IFactory factory;

    [SerializeField] List<ActionBase> actions;

    public void Start()
    {
        GameObject obj = GameObject.Find("/DontDestroyOnLoad");
        global = obj.GetComponent<IGlobal>();
        factory = obj.GetComponent<IFactory>();

        List<IActor> actors = new List<IActor>();

        global.StartCoroutine(Activate(global, factory, actors));
    }

    public override IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        foreach (ActionBase action in actions)
            yield return action.Activate(global, factory, actors);
    }
}
