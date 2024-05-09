using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.DontDestroyOnLoad
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        bool isMarkedDontDestroyOnLoad = false;

        public void Awake()
        {
            if (isMarkedDontDestroyOnLoad)
                return;

            isMarkedDontDestroyOnLoad = true;

            DontDestroyOnLoad(this);
        }
    }
}