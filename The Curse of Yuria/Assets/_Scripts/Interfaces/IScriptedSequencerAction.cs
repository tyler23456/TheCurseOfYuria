using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IScriptedSequencerAction
{
    bool isFinished { get; }

    Action onStart { get; set; }
    Action onUpdate { get; set; }
    Action onStop { get; set; }
    Action onFinish { get; set; }

    IEnumerator Activate();
}
