using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ScriptedSequencerActionSO : ScriptableObject, ICutsceneAction
{
    public abstract bool isFinished { get; protected set; }

    public abstract Action onStart { get; set; }
    public abstract Action onUpdate { get; set; }
    public abstract Action onStop { get; set; }
    public abstract Action onFinish { get; set; }

    public abstract IEnumerator Activate();
}
