using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectIcon : StatusEffectBase
{
    [SerializeField] protected Sprite icon;

    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);

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

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        if (icon == null)
            return;

        Destroy(target.obj.transform.Find(icon.name).gameObject);
    }
}