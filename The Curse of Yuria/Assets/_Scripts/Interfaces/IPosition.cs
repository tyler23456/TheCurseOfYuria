using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPosition
{
    bool isActive { get; set; }
    void Set(Vector2 position);
    void Add(Vector2 offset, ForceMode2D forceMode2D);
    void FixedUpdate();
    public void Update();
}
