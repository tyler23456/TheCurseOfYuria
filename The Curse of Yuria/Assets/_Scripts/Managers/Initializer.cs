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

            LoadingDisplay.Instance.ShowExclusivelyInParent(3, Vector2.zero, 0f);
        }
    }
}