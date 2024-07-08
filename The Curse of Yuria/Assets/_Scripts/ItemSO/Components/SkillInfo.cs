using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Items
{
    [System.Serializable]
    public class SkillInfo
    {
        [SerializeField] public int power;
        [SerializeField] public ArmType armType;
        [SerializeField] public ElementType elementType;
        [SerializeField] public CalculationType calculationType;
        [Space(5)] [SerializeField] public List<BonusType> bonusTypes;
        [Space(5)] [SerializeField] protected ParticleSystem particleSystem;

        public IEnumerator PerformAnimation(IActor user, IActor target, IItem item, StatusEffectsInfo statusEffectsInfo)
        {
            yield return new WaitForSeconds(0.5f);

            if (statusEffectsInfo.CheckForStatusEffectCounters(user, target, item))
                yield break;

            yield return PerformEffect(user, target, statusEffectsInfo);
        }

        public IEnumerator PerformEffect(IActor user, IActor target, StatusEffectsInfo statusEffectsInfo)
        {
            ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.obj.transform).GetComponent<ParticleSystem>();
            GameObject.Destroy(particleSystem.gameObject, particleSystem.main.duration);

            while (particleSystem.time < particleSystem.main.duration / 10f)
                yield return new WaitForEndOfFrame();

            if (user == null)
                yield break;

            if (statusEffectsInfo.IsInvalidTarget(target))
                yield break;

            float accumulator = 0;
            accumulator = elementType.Calculate(user, target, power * IStats.powerMultiplier);
            accumulator = armType.Calculate(user, target, accumulator);

            foreach (BonusType bonusType in bonusTypes)
                accumulator = bonusType.Calculate(user, target, accumulator);

            accumulator = calculationType.Calculate(user, target, accumulator);

            statusEffectsInfo.CheckStatusEffects(target);
        }

        public IEnumerator PerformEffect(IActor target)
        {
            ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.obj.transform).GetComponent<ParticleSystem>();
            GameObject.Destroy(particleSystem.gameObject, particleSystem.main.duration);

            while (particleSystem.time < particleSystem.main.duration / 10f)
                yield return new WaitForEndOfFrame();

            if (target == null)
                yield break;

            float accumulator = 0;
            accumulator = elementType.Calculate(null, target, power * IStats.powerMultiplier);
            accumulator = calculationType.Calculate(null, target, accumulator);
        }


        public void SetDirection(IActor user, params IActor[] targets)
        {
            if (targets.Length == 0)
                return;

            Vector2 direction = (targets[0].obj.transform.position - user.obj.transform.position).normalized;

            if (user.obj.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (direction.x >= 0)
                    user.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                else
                    user.obj.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                if (direction.x >= 0)
                    user.obj.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                else
                    user.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }
}
