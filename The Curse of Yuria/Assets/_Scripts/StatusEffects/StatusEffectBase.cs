using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.StatusEffects
{
    public abstract class StatusEffectBase : ScriptableObject
    {
        [SerializeField] Sprite icon;
        [SerializeField] GameObject visualEffect;

        public abstract void Activate(IActor target, float duration);

        protected void AddEffect<T>(IActor target) where T : StatusEffectComponentBase
        {
            GameObject obj = new GameObject(icon.name);
            obj.transform.parent = target.getGameObject.transform;
            SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = icon;
            spriteRenderer.sortingOrder = 199;
            int count = target.getGameObject.GetComponents<IStatusEffect>().Length;
            obj.transform.localPosition = new Vector3(-3f + (1.5f * count), 1f, 0f);
            target.getGameObject.AddComponent<T>();
        }

        protected class StatusEffectComponentBase : MonoBehaviour , IStatusEffect
        {
            public float duration { get; set; } = float.PositiveInfinity;

            float startTime = 0f;

            protected virtual void Start()
            {
                startTime = Time.time;
            }

            protected virtual void Update()
            {
                if (Time.time < startTime + duration)
                    return;

                Destroy(this);
            }

            protected virtual void OnDestroy()
            {

            }
        }

    }
}