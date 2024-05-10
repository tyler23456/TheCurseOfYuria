using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatusEffectsTargeter", menuName = "Targeters/StatusEffectsTargeter")]
public class StatusEffectsTargeter : TargeterBase
{
    enum State { StatusEffect, StatusAilment, KnockedOut }
    
    [SerializeField] State state;

    public override List<IActor> GetTargets(Vector2 position)
    {
        base.GetTargets(position);

        List<IActor> results = null;

        switch (state)
        {
            case State.StatusEffect:
                results = targets.FindAll(i => i.getGameObject.GetComponent<IStatusEffect>() != null);
                break;
            case State.StatusAilment:
                results = targets.FindAll(i => i.getGameObject.GetComponent<IStatusAilment>() != null);
                break;
            case State.KnockedOut:
                results = targets.FindAll(i => i.getGameObject.GetComponent<IKockOut>() != null);
                break;
        }
        
        return results == null || results.Count == 0 ? null : new List<IActor> { results[Random.Range(0, results.Count)] };
    }
}
