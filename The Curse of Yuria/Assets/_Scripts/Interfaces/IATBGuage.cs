using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IATBGuage
{
    public bool isActive { get; }
    float getMaximumValue { get; }
    Action<float> onATBChanged { get; set; }
    void Reset();
    public void RaisePriority();
    public void LowerPriority();
}