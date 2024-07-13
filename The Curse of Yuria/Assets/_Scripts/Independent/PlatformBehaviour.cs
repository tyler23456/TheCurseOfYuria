using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Independent
{
    [RequireComponent(typeof(Collider2D))]
    public class PlatformBehaviour : MonoBehaviour
    {
        const float DisabledDuration = 2f;

        new Collider2D collider;

        float accumulator = 0f;

        private void Start()
        {
            collider = GetComponent<Collider2D>();
        }

        void Update()
        {
            if (collider.enabled)
                return;

            accumulator += Time.deltaTime;

            if (accumulator > DisabledDuration)
            {
                collider.enabled = true;
                accumulator = 0f;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            IActor player = collision.gameObject.GetComponent<IActor>();

            if (player == null)
                return;

            if (!Input.GetKeyDown(KeyCode.S))
                return;

            collider.enabled = false;
        }
    }
}