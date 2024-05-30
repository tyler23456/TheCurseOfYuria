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
    
    public override List<IActor> CalculateTargets(Vector2 position)
    {
        List<IActor> results = new List<IActor>();

        if (order == Order.before)
        {
            if (Global.Instance.pendingCommands.Count > 0)
                results.Add(type == Type.user ? Global.Instance.pendingCommands.First().user : Global.Instance.pendingCommands.First().targets[0]);
        }
        else
        {
            if (Global.Instance.successfulCommands.Count > 0)
                results.Add(type == Type.user ? Global.Instance.successfulCommands.First().user : Global.Instance.successfulCommands.First().targets[0]);
        }
        

        return results;
    }


}
