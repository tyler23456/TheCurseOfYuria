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

            Transform loadingDisplay = GameObject.Find("/DontDestroyOnLoad/Canvas/LoadingDisplay").transform;
            loadingDisplay.GetChild(0).name = "Lunn";
            loadingDisplay.gameObject.SetActive(true);
        }
    }
}