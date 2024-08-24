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
        obj.transform.parent = target.obj.transform.GetChild(3);
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = icon;
        spriteRenderer.sortingOrder = 515;
        int count = target.obj.transform.GetChild(3).childCount;
        obj.transform.position = target.getCollider2D.bounds.max + new Vector3(-3f + (1.5f * count), 0.2f, 0f);
    }

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        if (icon == null)
            return;

        Destroy(target.obj.transform.GetChild(3).Find(icon.name).gameObject);
    }
}
