using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class SettingsDisplay : MonoBehaviour
    {
        IGlobal global;
        IFactory factory;

        [SerializeField] RectTransform grid;

        InventoryUI inventory;

        void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        }
    }
}