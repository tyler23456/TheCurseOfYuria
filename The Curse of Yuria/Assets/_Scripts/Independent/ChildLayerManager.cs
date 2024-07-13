using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChildLayerManager : MonoBehaviour
{
    [SerializeField] int layer;
    [SerializeField] bool applyLayerChange = false;

    SpriteRenderer[] spriteRenderers;

    public void Update()
    {
        if (!applyLayerChange)
            return;

        applyLayerChange = false;

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            spriteRenderer.sortingOrder = layer;
    }
}
