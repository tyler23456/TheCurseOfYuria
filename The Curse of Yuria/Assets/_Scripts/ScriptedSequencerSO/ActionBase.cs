using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public abstract class ActionBase : ScriptedSequencerActionSO
{
    [SerializeField] protected CharacterNameBase characterName;

    public override bool isFinished { get; protected set; } = false;

    public override Action onStart { get; set; } = () => { };
    public override Action onUpdate { get; set; } = () => { };
    public override Action onStop { get; set; } = () => { };
    public override Action onFinish { get; set; } = () => { };

    public override IEnumerator Activate()
    {
        yield return null;
    }
}
