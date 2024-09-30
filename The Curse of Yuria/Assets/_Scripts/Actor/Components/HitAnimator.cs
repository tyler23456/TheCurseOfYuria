using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class HitAnimator
    {
        IActor actor;
        List<Color> colors = new List<Color>();
        SpriteRenderer[] spriteRenderers;

        bool isTinted = false;

        public HitAnimator(IActor actor, SpriteRenderer[] spriteRenderers)
        {
            this.actor = actor;
            this.spriteRenderers = spriteRenderers;

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                colors.Add(spriteRenderer.color);
        }

        public void Start()
        {
            actor.StartCoroutine(HitAnimation());
        }

        public IEnumerator HitAnimation()
        {
            isTinted = false;
            float accumulator = Time.unscaledTime;
            for (int n = 0; n < 3; n++)
            {
                isTinted = !isTinted;
                for (int i = 0; i < colors.Count; i++)
                {
                    if (isTinted)
                        spriteRenderers[i].color = Color.red;
                    else
                        spriteRenderers[i].color = colors[i];
                }
                    
                yield return new WaitForSecondsRealtime(0.1f);
            }
            for (int i = 0; i < colors.Count; i++)
                spriteRenderers[i].color = colors[i];

        }
    }
}