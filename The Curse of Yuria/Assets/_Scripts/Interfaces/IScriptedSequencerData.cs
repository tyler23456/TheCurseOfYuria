using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptedSequencerData
{
    static Queue<ActionSO> actions { get; private set; } = new Queue<ActionSO>();
}
