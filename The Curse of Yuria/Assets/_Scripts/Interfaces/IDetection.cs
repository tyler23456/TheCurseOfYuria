using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetection
{
    int getPriority { get; }
    void RaisePriority();
    void LowerPriority();
}
