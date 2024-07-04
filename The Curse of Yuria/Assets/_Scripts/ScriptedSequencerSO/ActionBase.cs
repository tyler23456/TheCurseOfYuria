using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public abstract class ActionBase : ScriptableObject, IScriptedSequencerAction
{
    [SerializeField] protected CharacterNameBase characterName;

    public virtual bool isFinished { get; protected set; } = false;

    public virtual Action onStart { get; set; } = () => { };
    public virtual Action onUpdate { get; set; } = () => { };
    public virtual Action onStop { get; set; } = () => { };
    public virtual Action onFinish { get; set; } = () => { };

    public virtual IEnumerator Activate()
    {
        yield return null;
    }
}
