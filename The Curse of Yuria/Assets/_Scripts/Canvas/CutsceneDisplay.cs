using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CutsceneDisplay : DisplayBase
{
    public static CutsceneDisplay Instance { get; protected set; }

    [SerializeField] Image promptImage;
    [SerializeField] TMP_Text promptText;

    public Queue<ActionBase> actions { get; private set; } = new Queue<ActionBase>();

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Global.Instance.gameState = Global.GameState.Stopped;
        Global.Instance.StartCoroutine(Activate(new List<IActor>()));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public IEnumerator Activate(List<IActor> actors)
    {
        while (Global.Instance.cutsceneActions.Count > 0)
            yield return Global.Instance.cutsceneActions.Dequeue().Activate(actors);

        gameObject.SetActive(false);
    }

    public void ShowExclusivelyInParent(ActionBase[] actions)
    {
        base.ShowExclusivelyInParent();

        this.actions = new Queue<ActionBase>(actions);
    }
}
