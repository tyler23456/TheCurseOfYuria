using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

[System.Serializable]
public class BasicInfo : MonoBehaviour
{
    [HideInInspector] [SerializeField] Sprite _icon;
    [HideInInspector] [SerializeField] GameObject _prefab;

    [Space(5)]
    [SerializeField] [TextArea(3, 10)] string description;
    [HideInInspector] [SerializeField] ItemSprite _itemSprite;

    [Space(5)]
    [SerializeField] int marketValue = 4;

    public Sprite icon { get { return _icon; } set { _icon = value; } }
    public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
    public ItemSprite itemSprite { get { return _itemSprite; } set { _itemSprite = value; } }

    public string getDescription => description;
    public int getWorth => marketValue;
}
