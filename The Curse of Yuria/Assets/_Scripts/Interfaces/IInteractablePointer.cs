using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractablePointer
{
    bool enabled { get; }
    GameObject gameObject { get; }
    string getAction { get; }

    void Interact(IActor player);
}
