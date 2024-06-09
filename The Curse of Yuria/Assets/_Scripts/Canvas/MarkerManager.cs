using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MarkerManager : MonoBehaviour
{
    public static MarkerManager instance { get; private set; }

    [SerializeField] GameObject markerPrefab;
    [SerializeField] RectTransform markerParent;
    [SerializeField] Camera mainCamera;

    public int count => markerParent.childCount;

    void Awake()
    {
        instance = this;
    }

    public void AddMarker(string identifier, string message = "")
    {
        Instantiate(markerPrefab, markerParent).transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
        SetMarkerIdentifierAt(count - 1, identifier);
    }

    public void AddMarker(string identifier, Vector3 worldPosition, string message = "")
    {
        Instantiate(markerPrefab, markerParent).transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
        SetMarkerIdentifierAt(count - 1, identifier);
        SetMarkerWorldPositionAt(count - 1, worldPosition);
        SetMarkerMessageAt(count - 1, message);
    }

    public void AddMarker(string identifier, Vector2 screenPosition, string message = "")
    {
        Instantiate(markerPrefab, markerParent).transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
        SetMarkerIdentifierAt(count - 1, identifier);
        SetMarkerScreenPositionAt(count - 1, screenPosition);
        SetMarkerMessageAt(count - 1, message);
    }

    void SetMarkerScreenPositionAt(int index, Vector2 screenPosition)
    {
        ((RectTransform)markerParent.GetChild(index)).position = screenPosition;
    }

    void SetMarkerWorldPositionAt(int index, Vector3 worldPosition)
    {
        ((RectTransform)markerParent.GetChild(index)).position = mainCamera.WorldToScreenPoint(worldPosition);
    }

    void SetMarkerMessageAt(int index, string message)
    {
        markerParent.GetChild(index).GetComponentInChildren<Text>().text = message;
    }

    void SetMarkerIdentifierAt(int index, string identifier)
    {
        markerParent.GetChild(index).name = identifier;
    }

    public int Count(string identifier)
    {
        int result = 0;
        foreach (Transform child in markerParent)
            if (child.name == identifier)
                result++;
        return result;
    }

    public void SetMarkerScreenPositionAt(string identifier, Vector2 screenPosition)
    {
        ((RectTransform)markerParent.Find(identifier)).position = screenPosition;
    }

    public void SetMarkerWorldPositionAt(string identifier, Vector3 worldPosition)
    {
        ((RectTransform)markerParent.Find(identifier)).position = mainCamera.WorldToScreenPoint(worldPosition);
    }

    public void SetMarkerMessageAt(string identifier, string message)
    {
        markerParent.Find(identifier).GetComponentInChildren<Text>().text = message;
    }

    public void DestroyAllMarkersWith(string identifier)
    {
        for (int i = markerParent.childCount - 1; i >= 0; i--)
            if (markerParent.GetChild(i).name == identifier)
                Destroy(markerParent.GetChild(i).gameObject);
    }

    public void DestroyAllMarkers()
    {
        for (int i = markerParent.childCount - 1; i >= 0; i--)
            Destroy(markerParent.GetChild(i).gameObject);
    }

}
