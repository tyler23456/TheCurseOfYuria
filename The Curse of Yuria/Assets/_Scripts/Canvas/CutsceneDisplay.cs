using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneDisplay : DisplayBase
{
    public static CutsceneDisplay Instance { get; protected set; }

    [SerializeField] new Camera camera;
    [SerializeField] Text promptName;
    [SerializeField] TMP_Text promptText;

    public Camera getCamera => camera;
    public Text getPromptName => promptName;
    public TMP_Text getPromptText => promptText;

    public Queue<ActionBase> actions { get; private set; } = new Queue<ActionBase>();
    Transform[] actorTransforms = new Transform[] { };

    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GameStateManager.Instance.Stop();
        StartCoroutine(Activate());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public IEnumerator Activate()
    {
        while (actions.Count > 0)
            yield return actions.Dequeue().Activate();

        gameObject.SetActive(false);
    }

    public void ShowExclusivelyInParent(ActionBase[] actions)
    {
        this.actions = new Queue<ActionBase>(actions);
        base.ShowExclusivelyInParent();
    }
}
