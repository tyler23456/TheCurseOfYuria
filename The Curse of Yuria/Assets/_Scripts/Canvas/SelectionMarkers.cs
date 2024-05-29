using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectionMarkers : MonoBehaviour
{
    public static SelectionMarkers instance { get; set; }

    [SerializeField] RectTransform markerParent;
    [SerializeField] Camera mainCamera;

    public int count => markerParent.childCount;

    void Awake()
    {
        instance = this;
    }

    public void AddMarker(string message = "")
    {
        Instantiate(Factory.instance.GetMenuIcon("SelectionMarker"), markerParent).transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
    }

    public void AddMarker(Vector3 worldPosition, string message = "")
    {
        Instantiate(Factory.instance.GetMenuIcon("SelectionMarker"), markerParent).transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
        SetMarkerWorldPositionAt(count - 1, worldPosition);
    }

    public void AddMarker(Vector2 screenPosition, string message = "")
    {
        Instantiate(Factory.instance.GetMenuIcon("SelectionMarker"), markerParent).transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
        SetMarkerScreenPositionAt(count - 1, screenPosition);
    }

    public void SetMarkerScreenPositionAt(int index, Vector2 screenPosition)
    {
        ((RectTransform)markerParent.GetChild(index)).position = screenPosition;
    }

    public void SetMarkerWorldPositionAt(int index, Vector3 worldPosition)
    {
        ((RectTransform)markerParent.GetChild(index)).position = mainCamera.WorldToScreenPoint(worldPosition);
    }

    public void SetMarkerMessageAt(int index, string message)
    {
        markerParent.GetChild(index).GetComponentInChildren<Text>().text = message;
    }

    public void DestroyAllMarkers()
    {
        for (int i = markerParent.childCount - 1; i >= 0; i--)
            Destroy(markerParent.GetChild(i).gameObject);
    }
    
}
