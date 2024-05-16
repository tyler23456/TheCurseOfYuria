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

            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            global.sceneIDToLoad = 3;
            global.scenePositionToStart = Vector2.zero;
            global.sceneEulerAngleZToStart = 0;
            global.ToggleDisplay(IGlobal.Display.LoadingDisplay);
        }
    }
}