using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewPreviousTargeter", menuName = "Targeters/PreviousTargeter")]
public class PreviousTargeter : TargeterBase
{
    enum Type { user, target }
    enum Order { before, after }

    [SerializeField] Type type;
    [SerializeField] Order order;

    public override IActor[] CalculateTargets(Vector2 position)
    {
        List<IActor> results = new List<IActor>();

        if (order == Order.before)
        {
            Transform pendingCommands = GameObject.Find("DontDestroyOnLoad/PendingCommands").transform;

            if (pendingCommands.childCount > 0)
                return results.ToArray();

            Command command = pendingCommands.GetChild(0).GetComponent<Command>();
            results.Add(type == Type.user ? command.user : command.targets[0]);

        }
        else
        {
            Transform successfulCommands = GameObject.Find("DontDestroyOnLoad/SuccessfulCommands").transform;

            if (successfulCommands.childCount > 0)
                results.ToArray();

            Command command = successfulCommands.GetChild(successfulCommands.childCount).GetComponent<Command>();
            results.Add(type == Type.user ? command.user : command.targets[0]);

        }
        return results.ToArray();
    }
}
