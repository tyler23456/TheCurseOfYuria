using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableTrigger : IInteractable
{
    string getAction { get; }
    bool enabled { get; }
    GameObject gameObject { get; }
}
