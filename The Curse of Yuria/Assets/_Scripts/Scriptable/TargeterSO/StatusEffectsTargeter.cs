using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewStatusEffectsTargeter", menuName = "Targeters/StatusEffectsTargeter")]
public class StatusEffectsTargeter : TargeterBase
{
    enum State { StatusEffect, StatusAilment, KnockedOut }
    
    [SerializeField] List<StatusEffectBase> statusEffects;

    public override List<IActor> GetTargets(Vector2 position)
    {
        base.GetTargets(position);

        List<IActor> results = null;

        results = targets.FindAll(target => statusEffects.Any(statusEffect => target.getStatusEffects.Contains(statusEffect.name)));
        
        return results == null || results.Count == 0 ? null : new List<IActor> { results[Random.Range(0, results.Count)] };
    }
}
