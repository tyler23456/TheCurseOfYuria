using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Canvas
{
    public class MainMenuManager : MonoBehaviour
    {
        IGlobal global;

        public void Start()
        {
            global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();

            global.ToggleDisplay(IGlobal.Display.MainMenuDisplay);
            //GameObject.Find("/DontDestroyOnLoad/AllieRoot").SetActive(false);
            
        }

        public void OnDestroy()
        {
            //GameObject.Find("/DontDestroyOnLoad/AllieRoot").SetActive(true);
        }
    }
}