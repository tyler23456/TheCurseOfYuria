using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ControllerStates
{
    public class ControllerStatesSFX
    {
        const float Duration = 0.4f;

        RaycastHit2D hit = new RaycastHit2D();

        float accumulator = 0f;
        
        public void UpdateStepSFX(AudioSource audioSource)
        {
            if (UpdateAccumulatorAndAccumulatorIsNotAtMax())
                return;

            hit = Physics2D.Raycast(audioSource.transform.position + Vector3.up * 0.05f, Vector3.down, 0.5f, LayerMask.GetMask("TileCollision"));

            if (hit.collider == null)
                return;

            string tag = hit.transform.gameObject.tag;

            ControllerStatesSFXManager.Instance.PlayStepSFX(tag, audioSource, 0.15f, 0.3f, 0.7f, 1.3f);
        }

        public void UpdateLandSFX(AudioSource audioSource)
        {
            hit = Physics2D.Raycast(audioSource.transform.position + Vector3.up * 0.05f, Vector3.down, 0.5f, LayerMask.GetMask("TileCollision"));

            if (hit.collider == null)
                return;

            string tag = hit.transform.gameObject.tag;

            ControllerStatesSFXManager.Instance.PlayStepSFX("Land" + tag, audioSource, 0.1f, 0.2f, 0.7f, 1.3f);
        }

        public void UpdateOther(AudioSource audioSource, string tag, float minVolume, float maxVolume, float minPitch, float maxPitch)
        {
            if (UpdateAccumulatorAndAccumulatorIsNotAtMax())
                return;

            ControllerStatesSFXManager.Instance.PlayStepSFX(tag, audioSource, minVolume, maxVolume, minPitch, maxPitch);
        }


        bool UpdateAccumulatorAndAccumulatorIsNotAtMax()
        {
            accumulator += Time.deltaTime;

            bool result = accumulator < Duration;

            if (!result)
                accumulator = 0f;

            return result;
        }

    }
}