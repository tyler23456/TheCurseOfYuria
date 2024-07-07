using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

[System.Serializable]
public class BasicInfo : MonoBehaviour
{
    [HideInInspector] [SerializeField] public Sprite icon;
    [HideInInspector] [SerializeField] public GameObject prefab;

    [Space(5)]
    [SerializeField] [TextArea(3, 10)] public string description;
    [HideInInspector] [SerializeField] public ItemSprite itemSprite;

    [Space(5)]
    [SerializeField] public int marketValue = 4;
}
