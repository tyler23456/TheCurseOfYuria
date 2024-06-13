using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.UserActors
{
    public class SpriteFlipper : ISpriteFlipper
    {
        [SerializeField] SpriteRenderer[] spriteRenderers;
        
        public SpriteFlipper(SpriteRenderer[] spriteRenderers)
        {
            this.spriteRenderers = spriteRenderers;
        }

        public void SetFlipX(bool flipX)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.flipX = flipX;
        }

        public void SetFlipY(bool flipY)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.flipY = flipY;
        }
    }
}