using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : ScriptableObject
{
    public bool isFinished { get; protected set; } = false;

    public virtual IEnumerator Activate(IGlobal global, IFactory factory, List<IActor> actors)
    {
        yield return null;
    }
}
