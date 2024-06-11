using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class HitAnimator
    {
        IActor actor;
        SpriteRenderer[] spriteRenderers;

        public HitAnimator(IActor actor, SpriteRenderer[] spriteRenderers)
        {
            this.actor = actor;
            this.spriteRenderers = spriteRenderers;
        }

        public void Start()
        {
            actor.StartCoroutine(HitAnimation());
        }

        public IEnumerator HitAnimation()
        {
            float accumulator = Time.unscaledTime;
            while (Time.unscaledTime < accumulator + 0.5f)
            {
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSecondsRealtime(0.05f);
            }
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.enabled = true;

        }
    }
}