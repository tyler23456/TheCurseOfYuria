using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactor
{
    string getTrigger { get; }
    string getReaction { get; }
}
