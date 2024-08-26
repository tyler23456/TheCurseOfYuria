using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllieMarkerManager : MonoBehaviour
{
    static AllieMarkerManager Instance { get; set; }

    [SerializeField] Transform allies;
    [SerializeField] GameObject markerPrefab;
    [SerializeField] RectTransform markerParent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Instantiate(markerPrefab, allies.GetChild(0).transform.position, Quaternion.identity, markerParent).name = "0";
    }

    private void LateUpdate()
    {
        foreach (Transform child in transform)
            child.position = allies.GetChild(Int32.Parse(child.name)).transform.position + Vector3.down * 0.15f;
    }

    public void AddMarker(int allieIndex)
    {
        Instantiate(markerPrefab, allies.GetChild(allieIndex).transform.position, Quaternion.identity, markerParent);
    }

    public void DestroyAllMarkers()
    {
        for (int i = markerParent.childCount - 1; i >= 0; i--)
            Destroy(markerParent.GetChild(i).gameObject);
    }
}
