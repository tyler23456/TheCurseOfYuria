using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractablePointer : IInteractable
{
    bool enabled { get; }
    GameObject gameObject { get; }
    string getAction { get; }
}
