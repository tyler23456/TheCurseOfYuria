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
            if (BattleManager.Instance.pendingCommands.Count > 0)
                results.Add(type == Type.user ? BattleManager.Instance.pendingCommands.First().user : BattleManager.Instance.pendingCommands.First().targets[0]);
        }
        else
        {
            if (BattleManager.Instance.successfulCommands.Count > 0)
                results.Add(type == Type.user ? BattleManager.Instance.successfulCommands.First().user : BattleManager.Instance.successfulCommands.First().targets[0]);
        }
        

        return results;
    }


}
