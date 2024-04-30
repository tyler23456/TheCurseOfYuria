using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactor
{
    enum Target { trigger, user, allie, enemy }

    string getTrigger { get; } //should be written as a command
    string getReaction { get; } //should be written as another command
}
