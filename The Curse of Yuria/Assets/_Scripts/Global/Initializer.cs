using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class Initializer : MonoBehaviour
    {
        bool hasLoaded = false;

        void Update()
        {
            if (hasLoaded)
                return;

            hasLoaded = true;

            Global.instance.sceneIDToLoad = 3;
            Global.instance.scenePositionToStart = Vector2.zero;
            Global.instance.sceneEulerAngleZToStart = 0;
            Global.instance.ToggleDisplay(Global.Display.Loading);
        }
    }
}