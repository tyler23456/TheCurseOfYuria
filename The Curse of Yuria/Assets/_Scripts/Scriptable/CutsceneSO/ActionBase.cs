using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ActionBase : ScriptableObject
{
    public bool isFinished { get; protected set; } = false;

    public Action onStart { get; set; } = () => { };
    public Action onUpdate { get; set; } = () => { };
    public Action onStop { get; set; } = () => { };
    public Action onFinish { get; set; } = () => { };

    public virtual IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        yield return null;
    }
}
