using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableTrigger
{
    string getAction { get; }
    bool enabled { get; }
    GameObject gameObject { get; }

    void Interact(IActor player);
}
