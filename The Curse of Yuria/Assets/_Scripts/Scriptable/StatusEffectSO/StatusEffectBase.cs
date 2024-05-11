using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class StatusEffectBase : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] GameObject visualEffect;

    public abstract void Activate(IActor target, float duration);

    protected void AddEffect<T>(IActor target) where T : EffectBase
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

    protected class EffectBase : MonoBehaviour
    {
        public float effectDuration { get; set; } = float.PositiveInfinity;
        public float tickDuration { get; set; } = float.PositiveInfinity;

        public Action OnStart { get; set; } = () => { };
        public Action OnUpdate { get; set; } = () => { };
        public Action OnFixedUpdate { get; set; } = () => { };
        public Action OnTickUpdate { get; set; } = () => { };
        public Action OnStop { get; set; } = () => { };

        float effectStartTime = 0f;
        float tickStartTime = 0f; 

        protected virtual void Start()
        {
            effectStartTime = Time.time;
            tickStartTime = Time.time;
            OnStart.Invoke();
        }

        protected virtual void Update()
        {
            OnUpdate.Invoke();

            if (Time.time > tickStartTime + tickDuration)
            {
                tickStartTime = Time.time;
                OnTickUpdate.Invoke();
            }

            if (Time.time > effectStartTime + effectDuration)
            {
                Destroy(this);
            }        
        }

        protected virtual void FixedUpdate()
        {
            OnFixedUpdate.Invoke();
        }

        protected virtual void OnDestroy()
        {
            OnStop.Invoke();
        }
    }

    protected class StatusEffect : EffectBase, IStatusEffect { }
    protected class StatusAilment : EffectBase, IStatusAilment { }
    protected class KnockOut : EffectBase, IKockOut { }

}
