using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TCOY.Items
{
    [System.Serializable]
    public class StatusEffectsInfo
    {
        [Space(5)] [SerializeField] public List<StatusEffectProbability> statusEffectProbabilities = new List<StatusEffectProbability>();

        public Effect CheckForStatusEffectCounters(IActor user, IActor target, IItem item)
        {
            List<Effect> effects = new List<Effect>();

            int i = 0;
            bool isEffectCounterable = false;
            foreach (string statusEffect in target.getStatusEffects.GetNames())
            {
                effects.Add(StatFXDatabase.Instance.Get(statusEffect).OnHit(user, target, item));

                if (effects.Last().itemCancellationFlag == true || effects.Last().isSuccessful)
                {
                    isEffectCounterable = true;
                    break;
                }
                i++;
            }       
            return isEffectCounterable? effects[i] : new Effect(user, target, item);
        }

        public void CheckStatusEffects(IActor target)
        {
            foreach (StatusEffectProbability statusEffectProbability in statusEffectProbabilities)
                if (UnityEngine.Random.Range(0f, 1f) < statusEffectProbability.probability)
                    statusEffectProbability.statusEffect.Activate(target);
        }

        public bool TrueForAnyStatusEffect(Func<StatusEffect, bool> predicate)
        {
            return statusEffectProbabilities.Find(i => predicate.Invoke(i.statusEffect)) != null;
        }

        public bool ContainsStatusEffectThatCanRemoveKO()
        {
            return TrueForAnyStatusEffect(i => i is IRestoration && ((IRestoration)i).ContainsStatusEffectToRemove(StatFXDatabase.Instance.getKnockOut.name));
        }

        public bool IsInvalidTarget(IActor target)
        {
            return target.enabled == false || !target.isActive || target.hasKOStatusEffect && !ContainsStatusEffectThatCanRemoveKO() || target.obj.activeSelf == false;
        }
    }
}
