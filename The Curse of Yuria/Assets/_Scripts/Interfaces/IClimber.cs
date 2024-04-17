using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClimber
{
    bool GetIsActive();
    public void SetIsActive(bool isActive);
    void Add(float offsetY);
}
