using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptedSequencerData
{
    static Queue<ICutsceneAction> actions { get; private set; } = new Queue<ICutsceneAction>();
}
