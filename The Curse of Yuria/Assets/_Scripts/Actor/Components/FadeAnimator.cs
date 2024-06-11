using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TCOY.UserActors
{
    public class FadeAnimator : IFadeAnimator
    {
        IActor actor;
        SpriteRenderer[] spriteRenderers;

        public Action<IActor> OnCoroutineUpdate { get; set; } = (actor) => { };
        public Action<IActor> OnCoroutineEnd { get; set; } = (actor) => { };

        public FadeAnimator(IActor actor, SpriteRenderer[] spriteRenderers)
        {
            this.actor = actor;    
            this.spriteRenderers = spriteRenderers;
        }

        public void Start(float startColorAlpha, float endingColorAlpha, float duration)
        {
            actor.StartCoroutine(FadeAnimation(startColorAlpha, endingColorAlpha, duration));
        }

        IEnumerator FadeAnimation(float startingColorAlpha, float endingColorAlpha, float duration)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.color = new Color(
                    spriteRenderer.color.r,
                    spriteRenderer.color.g,
                    spriteRenderer.color.b,
                    startingColorAlpha);

            float accumulator = 0f;
            float percentageComplete = 0f;
            while (accumulator < duration)
            {
                accumulator += Time.unscaledDeltaTime;
                percentageComplete = accumulator / duration;

                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                    spriteRenderer.color = new Color(
                        spriteRenderer.color.r,
                        spriteRenderer.color.g,
                        spriteRenderer.color.b,
                        Mathf.Lerp(startingColorAlpha, endingColorAlpha, percentageComplete));

                OnCoroutineUpdate.Invoke(actor);
                yield return null;
            }

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.color = new Color(
                    spriteRenderer.color.r,
                    spriteRenderer.color.g,
                    spriteRenderer.color.b,
                    endingColorAlpha);

            OnCoroutineEnd.Invoke(actor);

            OnCoroutineUpdate = (actor) => { };
            OnCoroutineEnd = (actor) => { };
        }
    }
}