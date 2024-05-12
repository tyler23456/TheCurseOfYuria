using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    float getDuration { get; }
    string name { get; set; }
    void Activate(IActor actor, float accumulator = 0f);
    void OnAdd(IActor actor);
    void OnRemove(IActor actor);
}
