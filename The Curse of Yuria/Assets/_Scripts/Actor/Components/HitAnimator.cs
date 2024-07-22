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
            isTinted = true;
            float accumulator = Time.unscaledTime;
            while (Time.unscaledTime < accumulator + 0.25f)
            {
                isTinted = !isTinted;
                for (int i = 0; i < colors.Count; i++)
                {
                    if (isTinted)
                        spriteRenderers[i].color = Color.red;
                    else
                        spriteRenderers[i].color = colors[i];
                }
                    
                yield return new WaitForSecondsRealtime(0.05f);
            }
            for (int i = 0; i < colors.Count; i++)
                spriteRenderers[i].color = colors[i];

        }
    }
}