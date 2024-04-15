using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotation
{
    void SetAxes(bool xAxis, bool yAxis);
    void Forward(Vector2 forward);
    void Add(Vector2 eulerAnglesOffset);
    void Forward(Vector2 relativeForward, Vector2 localForward);
}
