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

    public override bool ActivateAttack(IActor user, IActor target, IItem item)
    {
        if (Random.Range(0f, 1f) >= probability)
            return false;

        Move move = moves[Random.Range(0, moves.Count)];
        List<IActor> targets = move.getTargeter.CalculateTargets(user.obj.transform.position);

        int layer = user.obj.layer == LayerMask.NameToLayer("Allie") ? LayerMask.NameToLayer("Enemy") : LayerMask.NameToLayer("Allie");

        if (removeAllTargetsWithTheSameLayerAsUser)
            targets.RemoveAll(i => i.obj.layer == layer);

        if (targets.Count == 0)
            return false;

        BattleManager.Instance.AddCommand(new Command(user, move.getskill, new List<IActor> { targets[0] }));
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
