using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.AStar
{
    public class StepSFX
    {
        const float Duration = 0.4f;

        RaycastHit2D hit = new RaycastHit2D();
        Animator animator;
        AudioSource audioSource;

        float accumulator = 0f;

        public StepSFX(Animator animator, AudioSource audioSource)
        {
            this.animator = animator;
            this.audioSource = audioSource;
        }

        public void Update()
        {
            accumulator += Time.deltaTime;      

            if (accumulator < Duration)
                return;

            accumulator = 0f;

            int state = animator.GetInteger("State");

            if (state != 1 && state != 2)
                return;            

            hit = Physics2D.Raycast(animator.transform.position + Vector3.up * 0.05f, Vector3.down, 0.5f, LayerMask.GetMask("TileCollision"));

            if (hit.collider == null)
                return;

            string tag = hit.transform.gameObject.tag;

            StepSFXManager.Instance.Play(tag, audioSource);
        }
    }
}