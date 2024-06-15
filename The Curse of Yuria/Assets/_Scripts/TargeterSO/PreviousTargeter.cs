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
            if (BattleManager.Instance.pendingCommandsCount > 0)
                results.Add(type == Type.user ? BattleManager.Instance.PeekNextCommand().user : BattleManager.Instance.PeekNextCommand().targets[0]);
        }
        else
        {
            if (BattleManager.Instance.successfulCommandsCount > 0)
                results.Add(type == Type.user ? BattleManager.Instance.PeekPreviousCommand().user : BattleManager.Instance.PeekPreviousCommand().targets[0]);
        }
        

        return results;
    }


}
