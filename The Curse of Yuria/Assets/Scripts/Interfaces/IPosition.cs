using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPosition
{
    void Set(Vector2 position);
    void Add(Vector2 offset);
}
