using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public class StepSFX
    {
        const float Duration = 0.4f;

        RaycastHit2D hit = new RaycastHit2D();

        float accumulator = 0f;
        
        public void Update(AudioSource audioSource)
        {
            accumulator += Time.deltaTime;      

            if (accumulator < Duration)
                return;

            accumulator = 0f;           

            hit = Physics2D.Raycast(audioSource.transform.position + Vector3.up * 0.05f, Vector3.down, 0.5f, LayerMask.GetMask("TileCollision"));

            if (hit.collider == null)
                return;

            string tag = hit.transform.gameObject.tag;

            ControllerStatesSFXManager.Instance.PlayStepSFX(tag, audioSource);
        }
    }
}