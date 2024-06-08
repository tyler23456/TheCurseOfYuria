using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class StatusEffectBase : ScriptableObject, IStatusEffect
{
    [SerializeField] protected Sprite icon;
    [SerializeField] protected GameObject visualEffect;
    [SerializeField] protected float duration;

    public float getDuration => duration;
    public GameObject getVisualEffect => visualEffect;

    public virtual void Activate(IActor target, float accumulator = 0f)
    {
        target.getStatusEffects.Add(name, accumulator);
    }

    public virtual void OnAdd(IActor target)
    {
        if (icon == null)
            return;

        GameObject obj = new GameObject(icon.name);
        obj.transform.parent = target.obj.transform;
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = icon;
        spriteRenderer.sortingOrder = 199;
        int count = target.getStatusEffects.getCount;
        obj.transform.localPosition = new Vector3(-3f + (1.5f * count), 1f, 0f);  
    }

    public virtual void OnRemove(IActor target)
    {
        if (icon == null)
            return;

        Destroy(target.obj.transform.Find(icon.name).gameObject);
    }
}
