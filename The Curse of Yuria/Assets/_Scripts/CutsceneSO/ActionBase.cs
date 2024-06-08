using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public abstract class ActionBase : ScriptableObject
{
    [SerializeField] protected CharacterNameBase characterName;

    public bool isFinished { get; protected set; } = false;

    public Action onStart { get; set; } = () => { };
    public Action onUpdate { get; set; } = () => { };
    public Action onStop { get; set; } = () => { };
    public Action onFinish { get; set; } = () => { };

    public virtual IEnumerator Activate()
    {
        yield return null;
    }
}
