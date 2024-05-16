using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class ControlsDisplay : MonoBehaviour
    {
        IGlobal global;
        IFactory factory;

        [SerializeField] RectTransform grid;

        InventoryUI inventory;

        void OnEnable()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            factory = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>();
        }
    }
}