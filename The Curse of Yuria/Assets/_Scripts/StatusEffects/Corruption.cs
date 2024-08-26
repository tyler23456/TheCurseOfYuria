using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.StatusEffects
{
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

        public override Effect OnAttack(IActor user, IActor target, IItem item)
        {
            if (Random.Range(0f, 1f) >= probability)
                return new Effect(user, target, item);

            Move move = moves[Random.Range(0, moves.Count)];
            List<IActor> targets = new List<IActor>(move.targeter.CalculateTargets(user.getCollider2D.bounds.center));

            int layer = user.obj.layer == LayerMask.NameToLayer("Allie") ? LayerMask.NameToLayer("Enemy") : LayerMask.NameToLayer("Allie");

            if (removeAllTargetsWithTheSameLayerAsUser)
                targets.RemoveAll(i => i.obj.layer == layer);

            if (targets.Count == 0)
                return new Effect(user, target, item);

            return new Effect(user, targets[0], move.skill, false, true);
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
}
