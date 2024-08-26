using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager Instance { get; private set; }

    [SerializeField] ShakeData shakeData;

    Dictionary<string, ShakeData> shakeDatas = new Dictionary<string, ShakeData>();

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
