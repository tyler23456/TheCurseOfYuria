using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TCOY.Targeters
{
    [CreateAssetMenu(fileName = "NewStatusEffectsTargeter", menuName = "Targeters/StatusEffectsTargeter")]
    public class StatusEffectsTargeter : TargeterBase
    {
        enum State { StatusEffect, StatusAilment, KnockedOut }

        [SerializeField] List<StatusEffect> statusEffects;

        public override IActor[] CalculateTargets(Vector2 position, float targetCheckDistance = DefaultTargetCheckDistance)
        {
            base.CalculateTargets(position);

            List<IActor> results = new List<IActor>();

            results = targets.FindAll(target => statusEffects.Any(statusEffect => target.getStatusEffects.Contains(statusEffect.name)));

            if (results.Count > 0)
                return new IActor[] { results[Random.Range(0, results.Count)] };
            else
                return results.ToArray();
        }
    }
}
