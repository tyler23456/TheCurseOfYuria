using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCorruption", menuName = "StatusEffects/Corruption")]
public class Corruption : StatusEffectIcon
{
    [SerializeField] List<Move> moves;
    [SerializeField] [Range(0f, 1f)] float probability;
    [SerializeField] bool removeAllTargetsWithTheSameLayerAsUser = true;

    public override void Activate(IActor target, float accumulator = 0)
    {
        base.Activate(target, accumulator);
    }

    public override bool OnAttack(IActor user, IActor target, IItem item)
    {
        if (Random.Range(0f, 1f) >= probability)
            return false;

        Move move = moves[Random.Range(0, moves.Count)];
        List<IActor> targets = new List<IActor>(move.targeter.CalculateTargets(user.obj.transform.position));

        int layer = user.obj.layer == LayerMask.NameToLayer("Allie") ? LayerMask.NameToLayer("Enemy") : LayerMask.NameToLayer("Allie");

        if (removeAllTargetsWithTheSameLayerAsUser)
            targets.RemoveAll(i => i.obj.layer == layer);

        if (targets.Count == 0)
            return false;

        Command command = new Command(user, move.skill, targets[0]);
        IBattleData.pendingCommands.AddLast(command);
        return true;
    }

    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);
        target.getDetection.LowerPriority();
    }

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);
        target.getDetection.RaisePriority();
    }
}
