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
            yield return new WaitForSecondsRealtime(0.2f);
            armType.PlaySoundEffect(user.getAudioSource);
            yield return new WaitForSecondsRealtime(0.3f);

            Effect effect = statusEffectsInfo.CheckForStatusEffectCounters(user, target, item);
            if (effect.itemCancellationFlag)
                yield break;

            yield return PerformEffect(effect.user, effect.target, statusEffectsInfo);
        }

        public IEnumerator PerformEffect(IActor user, IActor target, StatusEffectsInfo statusEffectsInfo)
        {
            if (this.particleSystem != null)
            {
                ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.getCollider2D.bounds.center, Quaternion.identity).GetComponent<ParticleSystem>();
                particleSystem.transform.parent = target.obj.transform;

                GameObject.Destroy(particleSystem.gameObject, 10f);
                GameObject.Destroy(particleSystem, particleSystem.main.duration);

                while (particleSystem.time < particleSystem.main.duration / 10f)
                    yield return new WaitForEndOfFrame();
            }      

            if (user == null)
                yield break;

            if (statusEffectsInfo.IsInvalidTarget(target))
                yield break;

            statusEffectsInfo.CheckStatusEffects(target);

            float accumulator = 0;
            accumulator = elementType.Calculate(user, target, power * IStats.powerMultiplier);
            accumulator = armType.Calculate(user, target, accumulator);

            foreach (BonusType bonusType in bonusTypes)
                accumulator = bonusType.Calculate(user, target, accumulator);

            accumulator = calculationType.Calculate(user, target, accumulator);
            calculationType.PlaySoundEffect(target.getAudioSource);
        }

        public IEnumerator PerformEffect(IActor target, StatusEffectsInfo statusEffectsInfo)
        {
            if (statusEffectsInfo.IsInvalidTarget(target))
                yield break;

            if (this.particleSystem != null)
            {
                ParticleSystem particleSystem = GameObject.Instantiate(this.particleSystem.gameObject, target.obj.transform).GetComponent<ParticleSystem>();
                AudioSource audioSource = particleSystem.GetComponent<AudioSource>();
                GameObject.Destroy(particleSystem.gameObject, 10f);
                GameObject.Destroy(particleSystem, particleSystem.main.duration);
                GameObject.Destroy(audioSource, audioSource.clip.length);

                while (particleSystem.time < particleSystem.main.duration / 10f)
                    yield return new WaitForEndOfFrame();
            }

            if (statusEffectsInfo.IsInvalidTarget(target))
                yield break;

            float accumulator = 0;
            accumulator = elementType.Calculate(null, target, power * IStats.powerMultiplier);
            accumulator = calculationType.Calculate(null, target, accumulator);
        }


        public void SetDirection(IActor user, List<IActor> targets)
        {
            if (targets.Count == 0)
                return;

            user.RotateToward(targets[0].getCollider2D.bounds.center);
        }
    }
}
