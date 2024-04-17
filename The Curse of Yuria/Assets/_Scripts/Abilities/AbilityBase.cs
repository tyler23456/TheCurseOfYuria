using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class AbilityBase : ScriptableObject, IAbility
    {
        [SerializeField] int power = 0;
        [SerializeField] string type = "None";
        [SerializeField] StatusEffectBase[] statusEffects;

        public virtual void Use(IActor user, IActor[] targets)
        {
            /*foreach (IActor target in targets)
                foreach (StatusEffectBase statusEffect in statusEffects)
                    target.getGameObject.AddComponent<statusEffect>();*/


        }
    }
}