using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectionMarkers : MonoBehaviour
{
    public static SelectionMarkers instance { get; set; }

    [SerializeField] RectTransform markerParent;
    [SerializeField] Camera mainCamera;

    List<Collider2D> colliders;

    void Awake()
    {
        instance = this;
    }

    public void AddMarkerTo(Collider target)
    {
        Instantiate(target, markerParent);
    }

    public void Update()
    {
        for (int i = colliders.Count - 1; i >= 0; i--)
        {
            if (colliders[i] == null)
            {
                colliders.RemoveAt(i);
                continue;
            }

            Vector3 position = colliders[i].bounds.center + Vector3.up * colliders[i].bounds.extents.y;
            position = mainCamera.WorldToScreenPoint(position);

            ((RectTransform)markerParent.GetChild(i)).anchoredPosition = position;
                
        }
    }

    public void DestroyMarkers()
    {
        colliders.Clear();

        for (int i = markerParent.childCount - 1; i >= 0; i--)
            Destroy(markerParent.GetChild(i).gameObject);
    }
    
}
