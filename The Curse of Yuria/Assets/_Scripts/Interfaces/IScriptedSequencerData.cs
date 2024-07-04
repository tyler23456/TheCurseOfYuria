using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptedSequencerData
{
    static Queue<IScriptedSequencerAction> actions { get; private set; } = new Queue<IScriptedSequencerAction>();
}
