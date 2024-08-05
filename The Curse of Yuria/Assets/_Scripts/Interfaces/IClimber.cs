using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClimber
{
    bool enabled { get; }
    Vector2 position { get; }
}
