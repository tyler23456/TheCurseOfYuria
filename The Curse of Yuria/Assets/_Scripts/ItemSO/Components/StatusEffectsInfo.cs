using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;

namespace TCOY.Items
{
    [System.Serializable]
    public class StatusEffectsInfo : MonoBehaviour
    {
        [SerializeField] public List<StatusEffectProbability> statusEffectProbabilities;

        public ReadOnlyCollection<StatusEffectProbability> getStatusEffectProbabilities => statusEffectProbabilities.AsReadOnly();

        public bool CheckForStatusEffectCounters(IActor user, IActor target, IItem item)
        {
            List<bool> itemCancellationFlags = new List<bool>();
            foreach (string statusEffect in target.getStatusEffects.GetNames())
                itemCancellationFlags.Add(StatFXDatabase.Instance.Get(statusEffect).OnHit(user, target, item));

            if (itemCancellationFlags.Contains(true))
                return true;

            return false;
        }

        public void CheckStatusEffects(IActor target)
        {
            foreach (StatusEffectProbability statusEffectProbability in statusEffectProbabilities)
                if (UnityEngine.Random.Range(0f, 1f) < statusEffectProbability.getProbability)
                    statusEffectProbability.getStatusEffect.Activate(target);
        }

        public bool TrueForAnyStatusEffect(Func<IStatusEffect, bool> predicate)
        {
            return statusEffectProbabilities.Find(i => predicate.Invoke(i.getStatusEffect)) != null;
        }

        public bool ContainsStatusEffectThatCanRemoveKO()
        {
            return TrueForAnyStatusEffect(i => i is IRestoration && ((IRestoration)i).ContainsStatusEffectToRemove(StatFXDatabase.Instance.getKnockOut.name));
        }

        public bool IsInvalidTarget(IActor target)
        {
            return target.enabled == false && !ContainsStatusEffectThatCanRemoveKO() || target.obj.activeSelf == false;
        }
    }
}
