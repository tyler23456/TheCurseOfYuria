using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.StatusEffects
{
    [CreateAssetMenu(fileName = "NewRedirection", menuName = "StatusEffects/Redirection")]
    public class Redirection : StatusEffectBase, IStatusEffect
    {
        [SerializeField] ParticleSystem particleSystem;

        enum Type { Deflection, Reflection }

        [SerializeField] Type type;

        public override void Activate(IActor target, float duration)
        {
            base.Activate(target, duration);
        }

        public override Effect OnHit(IActor user, IActor target, IItem item)
        {
            bool isSuccessful = false;
            if (type == Type.Deflection && item is IMelee || type == Type.Reflection && item is IScroll)
            {
                if (user != target)
                {
                    ParticleSystem particleSystemInstance = Instantiate(particleSystem.gameObject, target.obj.transform).GetComponent<ParticleSystem>();
                    AudioSource audioSource = particleSystemInstance.GetComponent<AudioSource>();
                    particleSystemInstance.transform.position = target.getCollider2D.bounds.center;
                    GameObject.Destroy(particleSystemInstance.gameObject, 10f);
                    GameObject.Destroy(particleSystemInstance, particleSystemInstance.main.duration);
                    GameObject.Destroy(audioSource, audioSource.clip.length);
                    target = user;
                    isSuccessful = true;
                }
            }

            return new Effect(user, target, item, false, isSuccessful);
        }
    }
}
