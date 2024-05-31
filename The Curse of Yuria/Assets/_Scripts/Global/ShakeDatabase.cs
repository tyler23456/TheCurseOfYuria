using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class ShakeDatabase : MonoBehaviour
{
    public static ShakeDatabase Instance { get; private set; }

    [SerializeField] ShakeData shakeData;

    Dictionary<string, ShakeData> shakeDatas;

    void Awake()
    {
        Instance = this;

        shakeDatas.Add(shakeData.name, shakeData);
    }

    public ShakeData Get(string name)
    {
        return shakeDatas[name];
    }
}
