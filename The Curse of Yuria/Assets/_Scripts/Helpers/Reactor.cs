using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reactor : IReactor
{
    [SerializeField] string trigger;
    [SerializeField] string reaction;

    public string getTrigger => trigger;
    public string getReaction => reaction;
}
